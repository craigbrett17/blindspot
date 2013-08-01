using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Blindspot.Helpers
{
    public class SlidingStream : Stream
    {
        #region Other stream member implementations

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { return false; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        public override long Length
        {
            get { return _pendingSegments.Count; }
        }

        public override long Position
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        #endregion Other stream member implementations

        public SlidingStream()
        {
            ReadTimeout = -1;
        }

        private readonly object _readSyncRoot = new object();
        private readonly object _writeSyncRoot = new object();
        private readonly LinkedList<ArraySegment<byte>> _pendingSegments = new LinkedList<ArraySegment<byte>>();
        private readonly ManualResetEventSlim _dataAvailableResetEvent = new ManualResetEventSlim();

        public new int ReadTimeout { get; set; }

        public override int Read(byte[] buffer, int offset, int count)
        {
            //if (_dataAvailableResetEvent.Wait(ReadTimeout))
            //    return 0;

            if (_pendingSegments.Count == 0)
                return 0;

            lock (_readSyncRoot)
            {
                int currentCount = 0;
                int currentOffset = 0;

                while (currentCount <= count)
                {
                    ArraySegment<byte> segment = _pendingSegments.First.Value;
                    _pendingSegments.RemoveFirst();

                    int index = segment.Offset;
                    for (; index < segment.Count; index++)
                    {
                        if (currentOffset < offset)
                        {
                            currentOffset++;
                        }
                        else
                        {
                            if (currentCount < buffer.Length)
                                buffer[currentCount] = segment.Array[index];
                            currentCount++;
                        }
                    }

                    if (currentCount == count)
                    {
                        if (index < segment.Offset + segment.Count)
                        {
                            _pendingSegments.AddFirst(new ArraySegment<byte>(segment.Array, index, segment.Offset + segment.Count - index));
                        }
                    }

                    if (_pendingSegments.Count == 0)
                    {
                        _dataAvailableResetEvent.Reset();

                        return currentCount;
                    }
                }

                return currentCount;
            }
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            byte[] copy = new byte[count];
            Array.Copy(buffer, offset, copy, 0, count);
            lock (_readSyncRoot)
            {
                _pendingSegments.AddLast(new ArraySegment<byte>(copy));
            }
            _dataAvailableResetEvent.Set();
        }

    }
}