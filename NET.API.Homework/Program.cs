using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NET.API.Homework
{
    class Program
    {

        static async Task Main(string[] args)
        {
            var httpClient = new HttpClient();

            var httpResponce = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/users");

            if (httpResponce.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var contentString = await httpResponce.Content.ReadAsStringAsync();

                var users = JsonConvert.DeserializeObject<List<Models.User>>(contentString);

                var userId = users.Where(u => u.Name == "Mrs. Dennis Schulist").Select(u =>u.Id).FirstOrDefault();



                var httpClientAlbum = new HttpClient();

                var httpResponceAlbum = await httpClientAlbum.GetAsync("https://jsonplaceholder.typicode.com/albums");

                if (httpResponceAlbum.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var contentStringAlbum = await httpResponceAlbum.Content.ReadAsStringAsync();

                    var albums = JsonConvert.DeserializeObject<List<Models.Album>>(contentStringAlbum);

                    var albumIds = albums.Where(a => a.UserId == userId).Select(a => a.Id);



                    var httpClientPhoto = new HttpClient();

                    var httpResponcePhoto = await httpClientPhoto.GetAsync("https://jsonplaceholder.typicode.com/photos");

                    if (httpResponcePhoto.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var contentStringPhoto= await httpResponcePhoto.Content.ReadAsStringAsync();

                        var photos = JsonConvert.DeserializeObject<List<Models.Photo>>(contentStringPhoto);



                        foreach (var albumId in albumIds)
                        {

                            foreach (var photo in photos)
                            {
                                
                                if (photo.AlbumId == albumId)
                                {
                                    Console.WriteLine(photo.Id);
                                }
                            }
                        }


                    }

                }

            }



        }
    }
}
