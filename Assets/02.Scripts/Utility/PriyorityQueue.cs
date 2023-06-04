using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PriorityQueue<T> {
    private List<Tuple<T, float>> elements = new List<Tuple<T, float>>();

    public int Count => elements.Count;

    public void Enqueue(T item, float priority) {
        elements.Add(Tuple.Create(item, priority));
    }

    public T Dequeue(out float priority) {
        if (elements.Count == 0) {
            throw new InvalidOperationException("PriorityQueue is empty");
        }

        int bestIndex = 0;
        for (int i = 1; i < elements.Count; i++) {
            if (elements[i].Item2 < elements[bestIndex].Item2) {
                bestIndex = i;
            }
        }
        priority = elements[bestIndex].Item2;
        T bestItem = elements[bestIndex].Item1;
        elements.RemoveAt(bestIndex);
        return bestItem;
    }

    public T Dequeue() {
        return Dequeue(out float n);
    }

    public bool Contains(T item) {
        for (int i = 0; i < elements.Count; i++) {
            if (EqualityComparer<T>.Default.Equals(elements[i].Item1, item)) {
                return true;
            }
        }
        return false;
    }

}
