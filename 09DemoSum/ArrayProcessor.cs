namespace _09DemoSum
{
    using System;
    using System.Collections.Generic;
    using System.Numerics;
    using System.Threading;

    public class ArrayProcessor
    {
        private readonly int[] array;
        private readonly int nrOfElementsToProcess;
        private readonly int startIndex;
        private readonly object _lock = new object();

        private long[] threadLocalSumsArray;

        private class ThreadArrayData {
            public int threadId { get; set; }
            public int[] sourceArray { get; set; }
            public long[] destArray { get; set; }
            public int startIndex { get; set; }
            public int endIndex { get; set; }
        }

        public ArrayProcessor(int[] array, int startIndex, int nrOfElementsToProcess)
        {
            this.Sum = 0;

            this.array = array;
            this.startIndex = startIndex;
            this.nrOfElementsToProcess = nrOfElementsToProcess;
        }

        public BigInteger Sum { get; private set; }

        public void CalculateSum()
        {
            var to = this.startIndex + this.nrOfElementsToProcess;

            for (var i = this.startIndex; i < to; i++)
            {
                this.Sum += this.array[i];
            }
        }

        public void CalculateSumParallelFixed() {
            var half = this.startIndex + (this.nrOfElementsToProcess / 2);
            var end = this.nrOfElementsToProcess;

            Thread t1 = new Thread(() => {
                long threadSum = 0;
                for (int i = this.startIndex; i < half; i++) {
                    threadSum += this.array[i];
                }
                lock (_lock) {
                    this.Sum += threadSum;
                }
            });

            Thread t2 = new Thread(() => {
                long threadSum = 0;
                for (int i = half; i < end; i++) {
                    threadSum += this.array[i];
                }
                lock (_lock) {
                    this.Sum += threadSum;
                }
            });

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();
        }

        public void CalculateSumParallelParameterized(int numThreads) {
            List<Thread> threads = new List<Thread>();
            int elementsPerThread = (int)Math.Round((double)this.nrOfElementsToProcess / numThreads);

            this.threadLocalSumsArray = new long[numThreads];

            for (int i = 0; i < numThreads; i++) {
                Thread t = new Thread(this.ThreadLocalSum);
                threads.Add(t);
                int startIdx = i * elementsPerThread;
                int endIdx;
                if ((startIdx + elementsPerThread) > this.nrOfElementsToProcess) {
                    endIdx = nrOfElementsToProcess;
                } else {
                    endIdx = startIdx + elementsPerThread;
                }
                t.Start(new ThreadArrayData {
                    threadId = i,
                    sourceArray = this.array,
                    destArray = this.threadLocalSumsArray,
                    startIndex = startIdx,
                    endIndex = endIdx
                });
            }

            foreach (var thread in threads) {
                thread.Join();
            }

            foreach (var localSum in this.threadLocalSumsArray) {
                this.Sum += localSum;
            }
        }

        public void ThreadLocalSum(object data) {
            var arrayData = data as ThreadArrayData;
            //Console.WriteLine($"Thread: {arrayData.startIndex} -> {arrayData.endIndex}");
            long localSum = 0;
            for (int i = arrayData.startIndex; i < arrayData.endIndex; i++) {
                localSum += arrayData.sourceArray[i];
            }

            arrayData.destArray[arrayData.threadId] = localSum;
        }
    }
}