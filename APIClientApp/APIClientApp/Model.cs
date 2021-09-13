using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace APIClientApp
{

	public class SinglePostcodeResponse
	{
		[JsonProperty("status")]
		public int Status { get; set; }
		public Postcode result { get; set; }
	}

	public class BulkPostcodeResponse
	{
		[JsonProperty("status")]
		public int Status { get; set; }
		public Result[] @result { get; set; }
	}

	public class Result
	{
		public string query { get; set; }
		[JsonProperty("result")]
		public Postcode postcode { get; set; }
	}

	public class Postcode
	{
		public string postcode { get; set; }
		public int quality { get; set; }
		public int eastings { get; set; }
		public int northings { get; set; }
		public string country { get; set; }
		public string nhs_ha { get; set; }
		public float longitude { get; set; }
		public float latitude { get; set; }
		public string european_electoral_region { get; set; }
		public string primary_care_trust { get; set; }
		public string region { get; set; }
		public string lsoa { get; set; }
		public string msoa { get; set; }
		public string incode { get; set; }
		public string outcode { get; set; }
		public string parliamentary_constituency { get; set; }
		public string admin_district { get; set; }
		public object parish { get; set; }
		public object admin_county { get; set; }
		public string admin_ward { get; set; }
		public object ced { get; set; }
		public string ccg { get; set; }
		public string nuts { get; set; }
		public Codes codes { get; set; }
	}

	public class Codes
	{
		public string admin_district { get; set; }
		public string admin_county { get; set; }
		public string admin_ward { get; set; }
		public string parish { get; set; }
		public string parliamentary_constituency { get; set; }
		public string ccg { get; set; }
		public string ccg_id { get; set; }
		public string ced { get; set; }
		public string nuts { get; set; }
		public string lsoa { get; set; }
		public string msoa { get; set; }
		public string lau2 { get; set; }
	}

	////Outcodes

	public class OutcodeResponse
	{
		public int status { get; set; }
		public OutcodeResult result { get; set; }
	}

	public class OutcodeResult
	{
		public string outcode { get; set; }
		public float longitude { get; set; }
		public float latitude { get; set; }
		public int northings { get; set; }
		public int eastings { get; set; }
		public string[] admin_district { get; set; }
		public string[] parish { get; set; }
		public object[] admin_county { get; set; }
		public string[] admin_ward { get; set; }
		public string[] country { get; set; }
	}


}