using Facebook;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace WeddingChecklistNew.Lib
{
    public class FacebookAPI
    {
        public void Share()
        {
            var client = new FacebookClient("EAAL7vDN9p7gBABUSgZB3zKjrcfZAZCOEAULHIWmGkj5I3xWUVLm53ZBih1bPYd8MCGeRZBNpo3xIudgagsmvUPZBTBFfDbAehQbk5hJu3glbOTJg0WtR3UR4bs2rA9fnjZB9ZCcPfpfAhbzy7dL2vTK6q88LJ2PaKXbTZBUt4sBPyngZDZD");
            dynamic parameters = new ExpandoObject[1];
            parameters[0] = new ExpandoObject();
            parameters[0].message = "test";
            //parameters.link = "http://www.example.com/article.html";
            //parameters.picture = "http://www.example.com/article-thumbnail.jpg";
            parameters[0].name = "Article Title";
            parameters[0].caption = "test";
            parameters[0].description = "test";
            //parameters.actions = new {
            //    name = "View on Zombo",
            //    link = "http://www.zombo.com",
            //};
            //parameters[0].privacy = new {
            //    value = "ALL_FRIENDS",
            //};
            //parameters[0].targeting = new {
            //    countries = "US",
            //    regions = "6,53",
            //    locales = "6",
            //};
            dynamic result = client.Post("me/feed", parameters);
        }
    }
}