﻿using Lunalipse.Utilities;
using Lunalipse.Common.Data;
using TL = TagLib;
using System.IO;
using Lunalipse.Common;
using System.Windows.Media.Imaging;
using Lunalipse.Common.Interfaces.IMetadata;

namespace Lunalipse.Core.Metadata
{
    public class MediaMetaDataReader : IMediaMetadataReader
    {
        public MusicEntity CreateEntity(string path)
        {
            TL.File media = TL.File.Create(path);
            MusicEntity me = new MusicEntity()
            {
                Album = media.Tag.Album,
                Artist = media.Tag.Performers,
                Extension = Path.GetExtension(path),
                Name = Path.GetFileNameWithoutExtension(path),
                Year = media.Tag.Year.ToString(),
                Path = path,
            };
            media.Dispose();
            return me;
        }

        public TL.File CreateRaw(string path)
        {
            return TL.File.Create(path);
        }

        public static BitmapSource GetPicture(string path)
        {
            TL.File media = TL.File.Create(path);
            BitmapSource bs = getPic(media.Tag);
            media.Dispose();
            return bs;
        }

        private static BitmapSource getPic(TL.Tag tag)
        {
            if (tag.Pictures == null || tag.Pictures.Length == 0) return null;
            return Graphic.Byte2BitmapSource(tag.Pictures[0].Data.Data);
        }

        
    }
}
