using EventGridEmulator.Contracts;
using System.Text;
using System.Text.Json;

namespace EventGridEmulator.Logic
{
	public class EventRequestParser
	{
		private readonly Dictionary<string, TopicConfiguration> _topicsLookup;
		private readonly ILogger _logger;

		public EventRequestParser(IEnumerable<TopicConfiguration>? topicConfigurations, ILogger logger)
		{
			if (topicConfigurations is null) { throw new Exception("This should never happen"); }
			_topicsLookup = topicConfigurations.ToDictionary(t => t.Name);
			_logger = logger;
		}

		public TopicConfiguration? RetrieveTopicConfigurationFromUrl(string rawUrl)
		{
			var topicName = ExtractTopicNameFromUrl(rawUrl);

			if (string.IsNullOrEmpty(topicName))
			{
				_logger.LogError($"Invalid request url format: '{rawUrl}'");
				return null;
			}

			if (_topicsLookup.ContainsKey(topicName) == false)
			{
				_logger.LogError($"Topic '{topicName}' not found in configuration.");
				return null;
			}

			_logger.LogInfo($"Request received for topic '{topicName}'");
			return _topicsLookup[topicName];
		}

		public async Task<IEnumerable<EventGridEvent>> ReadEventsFromStreamAsync(Stream stream, Encoding encoding)
		{
			using (var sr = new StreamReader(stream, encoding))
			{
				var body = await sr.ReadToEndAsync();
				var events = JsonSerializer.Deserialize<EventGridEvent[]>(body);
				if (events is null)
				{
					return new List<EventGridEvent>();
				}
				return events;
			}
		}

		private string? ExtractTopicNameFromUrl(string rawUrl)
		{
			var components = rawUrl.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

			if (components.Length == 1)
				return components[0];

			return null;
		}
	}
}
