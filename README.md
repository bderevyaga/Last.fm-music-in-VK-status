# Last.fm_music_in_VK_status

Програма розроблена доя того щоб інформація про пісню яка зараз грає в Last.fm відображалась також в статусі vk.com. Так щоб інші теж могли посхати дану композицію на свому компютері використовуючи сайт vk.com звісно якщо вона вже є в базі зайту.

По-замовчуванні в коді програми прописано посилання на мій акаунт Last.fm. Але це можна змінити, замінивши дану стрічку коду на свою.

```sh
string lastFm = GET("http://ws.audioscrobbler.com/2.0/?method=user.getrecenttracks&user=DonetSSS&api_key=72eea7cc279bbb9e1ffb4515acfd052b");
```

В результаті чого програма обробляє отриманий XML.

```xml
<lfm status="ok">
<recenttracks user="DonetSSS" page="1" perPage="10" totalPages="396" total="3951">
<track nowplaying="true">
<artist mbid="">Sun King</artist>
<name>Rory</name>
<streamable>0</streamable>
<mbid/>
<album mbid=""/>
<url>http://www.last.fm/music/Sun+King/_/Rory</url>
<image size="small"/>
<image size="medium"/>
<image size="large"/>
<image size="extralarge"/>
</track>
<track>...</track>
<track>...</track>
<track>...</track>
<track>...</track>
<track>...</track>
<track>...</track>
<track>...</track>
<track>...</track>
<track>...</track>
<track>...</track>
</recenttracks>
</lfm>
```

І з даного коду ми беремо дані про артиста і пісню

```xml
<artist mbid="">Sun King</artist>
<name>Rory</name>
```
