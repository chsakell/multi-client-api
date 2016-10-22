<h3>Building multi-client APIs in ASP.NET</h3>
<a href="http://wp.me/p3mRWu-15c" rel="attachment wp-att-4229" target="_blank"><img src="https://chsakell.files.wordpress.com/2016/02/multi-client-api-06.jpg" alt="multi-client-api-06" width="600" height="350" class="alignnone size-full wp-image-4229" /></a>
<hr/>
The project introduces a <a href="https://github.com/chsakell/multi-client-api/blob/master/src/ShapingAPI/Infrastructure/Core/Utils.cs" target="_blank">method</a> that processes JTokens in order to return only the properties requested by the client.
<ul>
<li>Usage</li>
</ul>
Assuming the result of the resource uri <strong><i>/api/tracks/1</i></strong> is the following:
```
{
  "TrackId": 1,
  "AlbumId": 1,
  "Bytes": 11170334,
  "Composer": "Angus Young, Malcolm Young, Brian Johnson",
  "GenreId": 1,
  "MediaTypeId": 1,
  "Milliseconds": 343719,
  "Name": "For Those About To Rock (We Salute You)",
  "UnitPrice": 0.99
}
```
You can request only specific properties of that resource by making the request <strong><i>/api/tracks/1?props=bytes,milliseconds,name</i></strong>
```
{
  "Bytes": 11170334,
  "Milliseconds": 343719,
  "Name": "For Those About To Rock (We Salute You)"
}
```
The algorithm supports nested navigation properties as well. If <strong><i>/api/albums/1</i></strong> returns..
```
{
  "AlbumId": 1,
  "ArtistName": "AC/DC",
  "Title": "For Those About To Rock We Salute You",
  "Track": [
    {
      "TrackId": 1,
      "AlbumId": 1,
      "Bytes": 11170334,
      "Composer": "Angus Young, Malcolm Young, Brian Johnson",
      "GenreId": 1,
      "MediaTypeId": 1,
      "Milliseconds": 343719,
      "Name": "For Those About To Rock (We Salute You)",
      "UnitPrice": 0.99
    },
    {
      "TrackId": 6,
      "AlbumId": 1,
      "Bytes": 6713451,
      "Composer": "Angus Young, Malcolm Young, Brian Johnson",
      "GenreId": 1,
      "MediaTypeId": 1,
      "Milliseconds": 205662,
      "Name": "Put The Finger On You",
      "UnitPrice": 0.99
    }
  ]
}
```
Then <strong><i>/api/albums/1?props=artistname,title,track(composer;name)</i></strong> should return the following:
```
{
  "ArtistName": "AC/DC",
  "Title": "For Those About To Rock We Salute You",
  "Track": [
    {
      "Composer": "Angus Young, Malcolm Young, Brian Johnson",
      "Name": "For Those About To Rock (We Salute You)"
    },
    {
      "Composer": "Angus Young, Malcolm Young, Brian Johnson",
      "Name": "Put The Finger On You"
    }
  ]
}
```
Properties in navigations should be semicolon (;) separated inside parethensis. 
<ul>
<li>Example in API Controller</li>
</ul>
```
var _tracks = _trackRepository.GetAll(includeProperties).Skip(page).Take(pageSize);

                var _tracksVM = Mapper.Map<IEnumerable<Track>, IEnumerable<TrackViewModel>>(_tracks);

                string _serializedTracks = JsonConvert.SerializeObject(_tracksVM, Formatting.None,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });

                JToken _jtoken = JToken.Parse(_serializedTracks);
                if (!string.IsNullOrEmpty(props))
                    Utils.FilterProperties(_jtoken, props.ToLower().Split(',').ToList());

                return Ok(_jtoken);
```
<p>
The project is built in Visual Studio 2015 and ASP.NET Core but the technique and the method can be easily integrated in any version of ASP.NET API. In case you want to run the <i>ShapingAPI</i> application:
<ol>
<li>
Download the source code and open the solution in Visual Studio 2015
</li>
<li>
Restore Nuget and Bower packages
</li>
<li>
Install the <a href="https://chinookdatabase.codeplex.com/" target="_blank">Chinook</a> database in your SQL Server by running the script inside the <a href="https://github.com/chsakell/multi-client-api/tree/master/src/ShapingAPI/SQL" target="_blank">SQL</a> folder.
</li>
<li>
Alter the <i>appsettings.json</i> file to reflect your database environment.
</li>
<li>
Run the application
</li>
</ol>
</p>

<h2>Donations</h2>
For being part of open source projects and documenting my work here and on <a href="https://chsakell.com">chsakell's blog</a> I really do not charge anything. I try to avoid any type of ads also.

If you think that any information you obtained here is worth of some money and are willing to pay for it, feel free to send any amount through paypal.

<table>
<tr><th>Paypal</th></tr>
<tbody>
<tr>
<td><a href="https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=chsakell%40gmail%2ecom&lc=US&item_name=Donation%20for%20chsakell%27s%20blog&currency_code=USD&bn=PP%2dDonationsBF%3abtn_donateCC_LG%2egif%3aNonHosted" style="text-align:center;display:block">
<img src="https://www.paypalobjects.com/webstatic/en_US/btn/btn_donate_cc_147x47.png" alt="Buy me a beer" />
</a></td>
</tr>
</tbody>
</table>

<h3 style="font-weight:normal;">Follow chsakell's Blog</h3>
<table id="gradient-style" style="box-shadow:3px -2px 10px #1F394C;font-size:12px;margin:15px;width:290px;text-align:left;border-collapse:collapse;" summary="">
<thead>
<tr>
<th style="width:130px;font-size:13px;font-weight:bold;padding:8px;background:#1F1F1F repeat-x;border-top:2px solid #d3ddff;border-bottom:1px solid #fff;color:#E0E0E0;" align="center" scope="col">Facebook</th>
<th style="font-size:13px;font-weight:bold;padding:8px;background:#1F1F1F repeat-x;border-top:2px solid #d3ddff;border-bottom:1px solid #fff;color:#E0E0E0;" align="center" scope="col">Twitter</th>
</tr>
</thead>
<tfoot>
<tr>
<td colspan="4" style="text-align:center;">Microsoft Web Application Development</td>
</tr>
</tfoot>
<tbody>
<tr>
<td style="padding:8px;border-bottom:1px solid #fff;color:#FFA500;border-top:1px solid #fff;background:#1F394C repeat-x;">
<a href="https://www.facebook.com/chsakells.blog" target="_blank"><img src="https://chsakell.files.wordpress.com/2015/08/facebook.png?w=120&amp;h=120&amp;crop=1" alt="facebook" width="120" height="120" class="alignnone size-opti-archive wp-image-3578"></a>
</td>
<td style="padding:8px;border-bottom:1px solid #fff;color:#FFA500;border-top:1px solid #fff;background:#1F394C repeat-x;">
<a href="https://twitter.com/chsakellsBlog" target="_blank"><img src="https://chsakell.files.wordpress.com/2015/08/twitter-small.png?w=120&amp;h=120&amp;crop=1" alt="twitter-small" width="120" height="120" class="alignnone size-opti-archive wp-image-3583"></a>
</td>
</tr>
</tbody>
</table>
<h2>License</h2>
Code released under the <a href="https://github.com/chsakell/multi-client-api/blob/master/licence" target="_blank"> MIT license</a>.
