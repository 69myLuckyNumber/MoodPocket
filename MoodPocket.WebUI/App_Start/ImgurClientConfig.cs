using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Models;
using Imgur.API.Models.Impl;

using System.Configuration;
using System.Threading.Tasks;

namespace MoodPocket.WebUI.App_Start
{
	public static class ImgurClientConfig
	{
		public static string IMGUR_CLIENT_ID { get; set; }
		public static string IMGUR_CLIENT_SECRET { get; set; }
		public static string IMGUR_ACCESS_TOKEN { get; set; }
		public static string IMGUR_TOKEN_TYPE { get; set; }
		public static string IMGUR_REFRESH_TOKEN { get; set; }
		public static string IMGUR_ACCOUNT_USERNAME { get; set; }
		public static string IMGUR_ACCOUNT_ID { get; set; }
		public static int IMGUR_EXPIRES_IN { get; set; }

		public static OAuth2Token AccessToken { get; set; }

		public static void Init()
		{
			IMGUR_CLIENT_ID = ConfigurationManager.AppSettings["CLIENT_ID"];
			IMGUR_CLIENT_SECRET = ConfigurationManager.AppSettings["CLIENT_SECRET"];
			IMGUR_ACCESS_TOKEN = ConfigurationManager.AppSettings["ACCESS_TOKEN"];
			IMGUR_REFRESH_TOKEN = ConfigurationManager.AppSettings["REFRESH_TOKEN"];
			IMGUR_TOKEN_TYPE = ConfigurationManager.AppSettings["TOKEN_TYPE"];
			IMGUR_ACCOUNT_USERNAME = ConfigurationManager.AppSettings["ACCOUNT_USERNAME"];
			IMGUR_ACCOUNT_ID = ConfigurationManager.AppSettings["ACCOUNT_ID"];
			IMGUR_EXPIRES_IN = int.Parse(ConfigurationManager.AppSettings["EXPIRES_IN"]);
			AccessToken = CreateToken();
		}
		private static OAuth2Token CreateToken()
		{
			var token = new OAuth2Token(IMGUR_ACCESS_TOKEN,
										IMGUR_REFRESH_TOKEN,
										IMGUR_TOKEN_TYPE,
										IMGUR_ACCOUNT_ID,
										IMGUR_ACCOUNT_USERNAME,
										IMGUR_EXPIRES_IN);
			return token;
		}

		//Use it only if your token is expired
		public static Task<IOAuth2Token> RefreshToken()
		{
			var client = new ImgurClient(IMGUR_CLIENT_ID, IMGUR_CLIENT_SECRET);
			var endpoint = new OAuth2Endpoint(client);
			var token = endpoint.GetTokenByRefreshTokenAsync(IMGUR_REFRESH_TOKEN);
			return token;
		}
	}
}