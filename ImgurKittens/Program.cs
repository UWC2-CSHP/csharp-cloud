using System.Text.Json;
using System.Text.Json.Serialization;

var client = new HttpClient();

client.BaseAddress = new Uri("https://api.imgur.com/3/gallery/t/");
client.DefaultRequestHeaders.Add("Accept", "application/json");

client.DefaultRequestHeaders.Add("Authorization", "Client-ID 2280591526449b5");

var result = await client.GetAsync("kitten");
if (result.IsSuccessStatusCode)
{
    var json = await result.Content.ReadAsStringAsync();

    try
    {
        var imgurResponseObject = JsonSerializer.Deserialize<Root>(json);

        Console.WriteLine(imgurResponseObject.Data.Items[0].Images[0].Link);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
public class AdConfig
{
    [JsonPropertyName("safeFlags")]
    public List<string> SafeFlags { get; set; }

    [JsonPropertyName("highRiskFlags")]
    public List<object> HighRiskFlags { get; set; }

    [JsonPropertyName("unsafeFlags")]
    public List<object> UnsafeFlags { get; set; }

    [JsonPropertyName("wallUnsafeFlags")]
    public List<object> WallUnsafeFlags { get; set; }

    [JsonPropertyName("showsAds")]
    public bool ShowsAds { get; set; }

    [JsonPropertyName("showAdLevel")]
    public int ShowAdLevel { get; set; }
}

public class Data
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("display_name")]
    public string DisplayName { get; set; }

    [JsonPropertyName("followers")]
    public int Followers { get; set; }

    [JsonPropertyName("total_items")]
    public int TotalItems { get; set; }

    [JsonPropertyName("following")]
    public bool Following { get; set; }

    [JsonPropertyName("is_whitelisted")]
    public bool IsWhitelisted { get; set; }

    [JsonPropertyName("background_hash")]
    public string BackgroundHash { get; set; }

    [JsonPropertyName("thumbnail_hash")]
    public object ThumbnailHash { get; set; }

    [JsonPropertyName("accent")]
    public string Accent { get; set; }

    [JsonPropertyName("background_is_animated")]
    public bool BackgroundIsAnimated { get; set; }

    [JsonPropertyName("thumbnail_is_animated")]
    public bool ThumbnailIsAnimated { get; set; }

    [JsonPropertyName("is_promoted")]
    public bool IsPromoted { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("logo_hash")]
    public object LogoHash { get; set; }

    [JsonPropertyName("logo_destination_url")]
    public object LogoDestinationUrl { get; set; }

    [JsonPropertyName("description_annotations")]
    public DescriptionAnnotations DescriptionAnnotations { get; set; }

    [JsonPropertyName("items")]
    public List<Item> Items { get; set; }
}

public class DescriptionAnnotations
{
    [JsonPropertyName("hashtag")]
    public List<Hashtag> Hashtag { get; set; }
}

public class Hashtag
{
    [JsonPropertyName("indices")]
    public List<int> Indices { get; set; }

    [JsonPropertyName("tag")]
    public string Tag { get; set; }
}

public class Image
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("title")]
    public object Title { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("datetime")]
    public int Datetime { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("animated")]
    public bool Animated { get; set; }

    [JsonPropertyName("width")]
    public int Width { get; set; }

    [JsonPropertyName("height")]
    public int Height { get; set; }

    [JsonPropertyName("size")]
    public int Size { get; set; }

    [JsonPropertyName("views")]
    public int Views { get; set; }

    [JsonPropertyName("bandwidth")]
    public object Bandwidth { get; set; }

    [JsonPropertyName("vote")]
    public object Vote { get; set; }

    [JsonPropertyName("favorite")]
    public bool Favorite { get; set; }

    [JsonPropertyName("nsfw")]
    public object Nsfw { get; set; }

    [JsonPropertyName("section")]
    public object Section { get; set; }

    [JsonPropertyName("account_url")]
    public object AccountUrl { get; set; }

    [JsonPropertyName("account_id")]
    public object AccountId { get; set; }

    [JsonPropertyName("is_ad")]
    public bool IsAd { get; set; }

    [JsonPropertyName("in_most_viral")]
    public bool InMostViral { get; set; }

    [JsonPropertyName("has_sound")]
    public bool HasSound { get; set; }

    [JsonPropertyName("tags")]
    public List<object> Tags { get; set; }

    [JsonPropertyName("ad_type")]
    public int AdType { get; set; }

    [JsonPropertyName("ad_url")]
    public string AdUrl { get; set; }

    [JsonPropertyName("edited")]
    public string Edited { get; set; }

    [JsonPropertyName("in_gallery")]
    public bool InGallery { get; set; }

    [JsonPropertyName("link")]
    public string Link { get; set; }

    [JsonPropertyName("mp4_size")]
    public int Mp4Size { get; set; }

    [JsonPropertyName("mp4")]
    public string Mp4 { get; set; }

    [JsonPropertyName("gifv")]
    public string Gifv { get; set; }

    [JsonPropertyName("hls")]
    public string Hls { get; set; }

    [JsonPropertyName("processing")]
    public Processing Processing { get; set; }

    [JsonPropertyName("comment_count")]
    public object CommentCount { get; set; }

    [JsonPropertyName("favorite_count")]
    public object FavoriteCount { get; set; }

    [JsonPropertyName("ups")]
    public object Ups { get; set; }

    [JsonPropertyName("downs")]
    public object Downs { get; set; }

    [JsonPropertyName("points")]
    public object Points { get; set; }

    [JsonPropertyName("score")]
    public object Score { get; set; }
}

public class Item
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("description")]
    public object Description { get; set; }

    [JsonPropertyName("datetime")]
    public int Datetime { get; set; }

    [JsonPropertyName("cover")]
    public string Cover { get; set; }

    [JsonPropertyName("cover_width")]
    public int CoverWidth { get; set; }

    [JsonPropertyName("cover_height")]
    public int CoverHeight { get; set; }

    [JsonPropertyName("account_url")]
    public string AccountUrl { get; set; }

    [JsonPropertyName("account_id")]
    public int AccountId { get; set; }

    [JsonPropertyName("privacy")]
    public string Privacy { get; set; }

    [JsonPropertyName("layout")]
    public string Layout { get; set; }

    [JsonPropertyName("views")]
    public int Views { get; set; }

    [JsonPropertyName("link")]
    public string Link { get; set; }

    [JsonPropertyName("ups")]
    public int Ups { get; set; }

    [JsonPropertyName("downs")]
    public int Downs { get; set; }

    [JsonPropertyName("points")]
    public int Points { get; set; }

    [JsonPropertyName("score")]
    public int Score { get; set; }

    [JsonPropertyName("is_album")]
    public bool IsAlbum { get; set; }

    [JsonPropertyName("vote")]
    public object Vote { get; set; }

    [JsonPropertyName("favorite")]
    public bool Favorite { get; set; }

    [JsonPropertyName("nsfw")]
    public bool Nsfw { get; set; }

    [JsonPropertyName("section")]
    public string Section { get; set; }

    [JsonPropertyName("comment_count")]
    public int CommentCount { get; set; }

    [JsonPropertyName("favorite_count")]
    public int FavoriteCount { get; set; }

    [JsonPropertyName("topic")]
    public object Topic { get; set; }

    [JsonPropertyName("topic_id")]
    public object TopicId { get; set; }

    [JsonPropertyName("images_count")]
    public int ImagesCount { get; set; }

    [JsonPropertyName("in_gallery")]
    public bool InGallery { get; set; }

    [JsonPropertyName("is_ad")]
    public bool IsAd { get; set; }

    [JsonPropertyName("tags")]
    public List<Tag> Tags { get; set; }

    [JsonPropertyName("ad_type")]
    public int AdType { get; set; }

    [JsonPropertyName("ad_url")]
    public string AdUrl { get; set; }

    [JsonPropertyName("in_most_viral")]
    public bool InMostViral { get; set; }

    [JsonPropertyName("include_album_ads")]
    public bool IncludeAlbumAds { get; set; }

    [JsonPropertyName("images")]
    public List<Image> Images { get; set; }

    [JsonPropertyName("ad_config")]
    public AdConfig AdConfig { get; set; }
}

public class Processing
{
    [JsonPropertyName("status")]
    public string Status { get; set; }
}

public class Root
{
    [JsonPropertyName("data")]
    public Data Data { get; set; }

    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("status")]
    public int Status { get; set; }
}

public class Tag
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("display_name")]
    public string DisplayName { get; set; }

    [JsonPropertyName("followers")]
    public int Followers { get; set; }

    [JsonPropertyName("total_items")]
    public int TotalItems { get; set; }

    [JsonPropertyName("following")]
    public bool Following { get; set; }

    [JsonPropertyName("is_whitelisted")]
    public bool IsWhitelisted { get; set; }

    [JsonPropertyName("background_hash")]
    public string BackgroundHash { get; set; }

    [JsonPropertyName("thumbnail_hash")]
    public object ThumbnailHash { get; set; }

    [JsonPropertyName("accent")]
    public string Accent { get; set; }

    [JsonPropertyName("background_is_animated")]
    public bool BackgroundIsAnimated { get; set; }

    [JsonPropertyName("thumbnail_is_animated")]
    public bool ThumbnailIsAnimated { get; set; }

    [JsonPropertyName("is_promoted")]
    public bool IsPromoted { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("logo_hash")]
    public object LogoHash { get; set; }

    [JsonPropertyName("logo_destination_url")]
    public object LogoDestinationUrl { get; set; }

    [JsonPropertyName("description_annotations")]
    public DescriptionAnnotations DescriptionAnnotations { get; set; }
}