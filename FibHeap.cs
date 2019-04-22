using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FibonacciHeap
{
    class FibHeap
    {
        public NodeTree Min { get; set; }
        public List<NodeTree> Roots { get; set; }
        public int NodesCount { get; set; }

        public FibHeap()
        {
            Min = null;
            Roots = new List<NodeTree>();
            NodesCount = 0;
        }

        /// <summary>
        /// Добавляет новый элемент в кучу.
        /// </summary>
        public void Insert(int x)
        {
            var a = new NodeTree(x)
            {
                Degree = 0,
                Parent = null,
                Mark = false,
            };

            if (this.Min == null)
            {
                this.Roots.Add(a);
                this.Min = a;
            }
            else
            {
                this.Roots.Add(a);
                if (x < this.Min.NodeData)
                    this.Min = a;
            }

            NodesCount++;
        }

        /// <summary>
        /// Сливает две кучи в одну.
        /// </summary>
        public FibHeap Union(FibHeap fib)
        {
            var unionFib = new FibHeap { Min = this.Min };
            this.Roots.AddRange(fib.Roots);
            unionFib.Roots = new List<NodeTree>(this.Roots);
            if ((this.Min == null) || (fib.Min != null && fib.Min.NodeData < this.Min.NodeData))
                unionFib.Min = fib.Min;
            unionFib.NodesCount = this.NodesCount + fib.NodesCount;
            return unionFib;
        }

        /// <summary>
        /// Извлекает минимальный элемент из кучи.
        /// </summary>
        public NodeTree ExtractMin()
        {
            var z = this.Min;
            if (z != null)
            {
                if (z.Childrens != null)
                {
                    foreach (var child in z.Childrens)
                    {
                        this.Roots.Add(child);
                        child.Parent = null;
                    }
                }
                this.Roots.Remove(z);

                if (Roots.Count == 0)
                    this.Min = null;
                else
                {
                    this.Min = Roots[0];
                    Consolidate(this);
                }

                this.NodesCount--;
            }
            return z;
        }

        public void Consolidate(FibHeap H)
        {
            var maxDegree = Convert.ToInt32(Math.Log10(NodesCount))+2;
            var A = new NodeTree[maxDegree];

            for (int i = 0; i < maxDegree; i++)
            {
                A[i] = null;
            }

            var rootsCount = Roots.Count;
            var tmpRoots = new List<NodeTree>(this.Roots);

            for (int i = 0; i<tmpRoots.Count; i++)
            {
                var x = tmpRoots[i];
                var d = x.Degree;
                while (A[d] != null)
                {
                    var y = A[d];
                    if (x.NodeData > y.NodeData)
                    {
                        var tmp = x;
                        x = y;
                        y = tmp;
                    }
                    FibLink(H, y, x);
                    A[d] = null;
                    d++;
                }
                A[d] = x;
                
            }

            H.Min = null;

            for (int i = 0; i<maxDegree; i++)
            {
                if (A[i] != null)
                    if (H.Min == null)
                    {
                        H.Min = A[i];
                    }
                    else
                    {
                        if (A[i].NodeData < H.Min.NodeData)
                            H.Min = A[i];
                    }
            }
        }

        public void FibLink(FibHeap H, NodeTree y, NodeTree x)
        {
            H.Roots.Remove(y);
            x.Childrens.AddLast(y);
            y.Parent = x;
            x.Degree++;
            y.Mark = false;
        }

        public void DecreaseKey(NodeTree x, int k)
        {
            if (k > x.NodeData)
                throw new Exception("Новый ключ больше текущего");
            x.NodeData = k;
            var y = x.Parent;
            if ((y != null) && (x.NodeData < y.NodeData))
            {
                Cut(x, y);
                CascadingCut(y);
            }
            if (x.NodeData < Min.NodeData)
                Min = x;     
        }

        public void Cut(NodeTree x, NodeTree y)
        {
            y.Childrens.Remove(x);
            y.Degree--;
            Roots.Add(x);
            x.Parent = null;
            x.Mark = false;
        }

        public void CascadingCut(NodeTree y)
        {
            var z = y.Parent;
            if (z != null)
            {
                if (y.Mark == false)
                    y.Mark = true;
                else
                {
                    Cut(y, z);
                    CascadingCut(z);
                }
            }
        }

        /// <summary>
        /// Удаляет указанный узел из кучи.
        /// </summary>
        public void Delete(NodeTree x)
        {
            DecreaseKey(x, int.MinValue);
            ExtractMin();
        }
    }
}
