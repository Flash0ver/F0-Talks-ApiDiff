//https://github.com/dotnet/runtime/issues/43957
//https://youtu.be/bvoCR1hXMfw?t=48

namespace F0.System.Collections.Generic;

public class PriorityQueueTests
{
	[Fact]
	public void PriorityQueue()
	{
		PriorityQueue<string, int> queue = new(Comparer<int>.Default);
		queue.EnsureCapacity(2).Should().Be(4);

		queue.Enqueue("1", 1);
		queue.Enqueue("2", 1);

		queue.Dequeue().Should().Be("1");
		queue.Dequeue().Should().Be("2");

		queue.Enqueue("1", 2);
		queue.Enqueue("2", 1);

		queue.Dequeue().Should().Be("2");
		queue.Dequeue().Should().Be("1");

		queue.EnqueueDequeue("3", 3)
			.Should().Be("3");

		queue.Count.Should().Be(0);
	}
}
