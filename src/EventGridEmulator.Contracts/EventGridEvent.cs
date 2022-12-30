namespace EventGridEmulator.Contracts
{
	public interface EventGridEvent
	{
		public string Topic { get; set; }
		public string Subject { get; set; }
		public object Data { get; set; }
		public string EventType { get; set; }
		public DateTime PublishTime { get; set; }
		public DateTime EventTime { get; set; }
		public string Id { get; set; }
	}
}
