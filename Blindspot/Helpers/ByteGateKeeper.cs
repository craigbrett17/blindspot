﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blindspot.Helpers
{
    /// <summary>
    /// An object that regulates the passing of music data from a store into a circular buffer
    /// </summary>
    public class ByteGateKeeper
    {
        // the sliding stream yet to be used
        private SlidingStream bytesInWaiting;
        // any set of bytes that weren't big enough to go through alone wait here
        private byte[] spareBytes;

        /// <summary>
        /// Gets or sets the minimum amount of data to be returned at any time
        /// </summary>
        public int MinimumSampleSize { get; set; }

        public ByteGateKeeper()
        {
            bytesInWaiting = new SlidingStream();
        }

        public ByteGateKeeper(int minimum)
        {
            bytesInWaiting = new SlidingStream();
            this.MinimumSampleSize = minimum;
        }

        /// <summary>
        /// Queues up the given data in the store
        /// </summary>
        /// <param name="bytes">The bytes to be queued</param>
        public void QueueBytes(byte[] bytes)
        {
            bytesInWaiting.Write(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// Removes all queued up bytes to start things afresh
        /// </summary>
        public void Clear()
        {
            bytesInWaiting = new SlidingStream();
            spareBytes = null;
        }

        /// <summary>
        /// Retrieves the next sample to be passed into the buffer
        /// </summary>
        /// <returns>An array of bytes that make up the sample</returns>
        public byte[] Read()
        {
            var sample = new byte[this.MinimumSampleSize];
            var bytesRead = bytesInWaiting.Read(sample, 0, sample.Length - 1);
            if (bytesRead > 0)
            {
                // if we already have some spare data kicking around, join it together
                if (spareBytes != null)
                {
                    var combinedSamples = new byte[spareBytes.Length + bytesRead];
                    // combine the spare bytes with the new sample in this new array
                    Buffer.BlockCopy(spareBytes, 0, combinedSamples, 0, spareBytes.Length);
                    Buffer.BlockCopy(sample, 0, combinedSamples, spareBytes.Length, bytesRead);
                    sample = combinedSamples;
                    bytesRead = sample.Length; // the sample's new length has no silence, so we can use it as bytesRead
                    spareBytes = null;
                }
                // if the sample isn't big enough to go in, wait for the next lot
                if (bytesRead < MinimumSampleSize)
                {
                    spareBytes = new byte[bytesRead];
                    // yeah, we're using byte arrays, so lightning fast Buffer block copies for us
                    Buffer.BlockCopy(sample, 0, spareBytes, 0, bytesRead);
                    return null;
                }
                else
                {
                    return sample;
                }
            }
            else
            {
                return null;
            }
        }

    }

    public class AngryGateKeeperException : Exception
    {
        // not sure what we want to do with this yet
    }
}