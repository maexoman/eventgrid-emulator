namespace EventGridEmulator.Contracts
{
	public class EmulatorConfiguration
	{
		public int Port { get; set; }
		public IEnumerable<TopicConfiguration> Topics { get; set; }
		public IEnumerable<DispatchStrategyConfiguration> DispatchStrategies { get; set; }

		public EmulatorConfiguration(
			int port,
			IEnumerable<TopicConfiguration> topics,
			IEnumerable<DispatchStrategyConfiguration> dispatchStrategies
		)
		{
			Port = port;
			Topics = topics;
			DispatchStrategies = dispatchStrategies;
		}
	}

	public class TopicConfiguration
	{
		public string Name { get; set; }
		public IEnumerable<SubscriptionConfiguration> Subscriptions { get; set; }

		public TopicConfiguration(
			string name,
			IEnumerable<SubscriptionConfiguration> subscriptions
		)
		{
			Name = name;
			Subscriptions = subscriptions;
		}
	}

	public class SubscriptionConfiguration
	{
		public string Name { get; set; }
		public IEnumerable<string> EventTypes { get; set; }
		public string SubjectBeginsWith { get; set; }
		public string SubjectEndsWith { get; set; }
		public string EndpointUrl { get; set; }
		public string DispatchStrategy { get; set; }

		public SubscriptionConfiguration(
			string name,
			IEnumerable<string> eventTypes,
			string subjectBeginsWith,
			string subjectEndsWith,
			string endpointUrl,
			string dispatchStrategy
		)
		{
			Name = name;
			EventTypes = eventTypes;
			SubjectBeginsWith = subjectBeginsWith;
			SubjectEndsWith = subjectEndsWith;
			EndpointUrl = endpointUrl;
			DispatchStrategy = dispatchStrategy;
		}
	}

	public class DispatchStrategyConfiguration
	{
		public string Name { get; set; }
		public string Type { get; set; }

		public DispatchStrategyConfiguration(string name, string type)
		{
			Name = name;
			Type = type;
		}
	}
}
