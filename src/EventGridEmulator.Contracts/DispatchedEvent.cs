namespace EventGridEmulator.Contracts
{
	public class DispatchedEvent
	{
		public string EndpointUrl { get; }
		public string DispatcherStrategy { get; }
		public EventGridEvent Payload { get; }

		public DispatchedEvent(
			string endpointUrl,
			string dispatcherStrategy,
			EventGridEvent payload
		)
		{
			EndpointUrl = endpointUrl;
			DispatcherStrategy = dispatcherStrategy;
			Payload = payload;
		}
	}
}
