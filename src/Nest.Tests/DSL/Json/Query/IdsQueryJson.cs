﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Nest.DSL;
using Nest.TestData.Domain;

namespace Nest.Tests.Dsl.Json.Filter
{
	[TestFixture]
	public class IdsQueryJson
	{
		[Test]
		public void IdsQuery()
		{
			var s = new SearchDescriptor<ElasticSearchProject>().From(0).Size(10)
				.Query(filter=>filter
					.Ids(new[] { "1", "4", "100" })
			);
				
			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
						ids : { 
							values : [""1"", ""4"", ""100""]
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}
		[Test]
		public void IdsFilterWithType()
		{
			var s = new SearchDescriptor<ElasticSearchProject>().From(0).Size(10)
				.Query(filter => filter
					.Ids("my_type", new[] { "1", "4", "100" })
			);

			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
						ids : { 
							type: [""my_type""],
							values : [""1"", ""4"", ""100""]
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}
		[Test]
		public void IdsFilterWithTypes()
		{
			var s = new SearchDescriptor<ElasticSearchProject>().From(0).Size(10)
				.Query(filter => filter
					.Ids(new []{"my_type", "my_other_type"}, new[] { "1", "4", "100" })
			);

			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
						ids : { 
							type: [""my_type"", ""my_other_type""],
							values : [""1"", ""4"", ""100""]
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}