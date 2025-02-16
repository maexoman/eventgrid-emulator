﻿using EventGridEmulator.Contracts;

namespace EventGridEmulator.Logic
{
	public class DispatcherStrategyFactory
	{
		private readonly Dictionary<string, DispatchStrategyConfiguration> _strategyLookups;
		private readonly ILogger _logger;
		private readonly Dictionary<string, IDispatcherStrategy?> _dispatchStrategies;

		public DispatcherStrategyFactory(IEnumerable<DispatchStrategyConfiguration> strategyConfigurations, ILogger logger)
		{
			_strategyLookups = strategyConfigurations.ToDictionary(s => s.Name);
			_logger = logger;
			_dispatchStrategies = new Dictionary<string, IDispatcherStrategy?>();
		}

		public IDispatcherStrategy? GetStrategy(string name)
		{
			if (!_dispatchStrategies.ContainsKey(name))
			{
				_dispatchStrategies[name] = CreateDispatchStratgey(name);
			}

			return _dispatchStrategies[name];
		}

		private IDispatcherStrategy? CreateDispatchStratgey(string name)
		{
			if (_strategyLookups.ContainsKey(name) == false)
			{
				_logger.LogError($"Invalid dispatch strategy name '{name}'.");
				return null;
			}

			var strategyConfig = _strategyLookups[name];
			var strategyType = typeof(DispatcherStrategyFactory).Assembly
								.GetTypes()
								.Single(t => t.FullName == strategyConfig.Type);

			var instance = Activator.CreateInstance(strategyType, _logger);
			return instance == null ? null : (IDispatcherStrategy) instance;
		}
	}
}
