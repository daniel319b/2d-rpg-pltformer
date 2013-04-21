using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Xml.Serialization;
using System.IO;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate;
using System.Xml;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace RPGPlatformerEngine
{
    public class TileMap
    {
        #region Description
        /// <summary>
        /// The name of the map.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The dimensions of the map(in tiles)
        /// </summary>
        public Point Dimensions { get; set; }
        /// <summary>
        /// The tile size in pixels.
        /// </summary>
        public Point TileSize { get; set; }
        /// <summary>
        /// The tile sheet assest name.
        /// </summary>
        public string TileSheetAssestName { get; set; }
        /// <summary>
        /// The collision tile texture assest name.
        /// </summary>
        public string CollisionTileTextureName { get; set; }
        /// <summary>
        /// A list of the background layers.(Assest name)
        /// </summary>
        public List<string> BackgroundLayerTextures { get; set; }
        #endregion

        #region Graphical Properties
        

        /// <summary>
        /// Defines the basic ground tiles array. Each tile type has a unique ID.
        /// </summary>
        public int[] BaseLayer { get; set; }
        /// <summary>
        /// Defines the objects array, used for decoration.
        /// </summary>
        public List<GameObject> ObjectLayer { get; set; }
        /// <summary>
        /// Defines the Collision array.
        /// </summary>
        public int[] CollisionLayer { get; set; }

        /// <summary>
        /// The background layer texture objects.
        /// </summary>
        [ContentSerializerIgnore]
        public List<Texture2D> BackgroundLayer { get; set; }

        /// <summary>
        /// The texture of the tile set.
        /// </summary>
        [ContentSerializerIgnore]
        public Texture2D TileSetTexture { get; set; }

        /// <summary>
        /// The collision tile texture.
        /// </summary>
        [ContentSerializerIgnore]
        public Texture2D CollisionTileTexture { get; set; }

        /// <summary>
        /// The bounding rectangle of the map area.
        /// </summary>
        [ContentSerializerIgnore]
        public Rectangle Bounds
        {
            get { return new Rectangle(0, 0, Dimensions.X * Tile.Width, Dimensions.Y * Tile.Height); }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Draw the base layer?
        /// </summary>
        [ContentSerializerIgnore]
        public bool DrawBaseLayer { get; set; }
        /// <summary>
        /// Draw the object layer?
        /// </summary>
        [ContentSerializerIgnore]
        public bool DrawObjectLayer { get; set; }
        /// <summary>
        /// Draw the background layer?
        /// </summary>
        [ContentSerializerIgnore]
        public bool DrawBackgroundLayer { get; set; }
        /// <summary>
        /// Draw the collision layer?
        /// </summary>
        [ContentSerializerIgnore]
        public bool DrawCollisionLayer { get; set; }

       

        public event EventHandler Created;

        /// <summary>
        /// The spawn point of the player in the map.
        /// </summary>
        public Vector2 SpawnPoint { get; set; }

        #endregion


        #region Intitialization

        /// <summary>
        /// Creates an empty map object.
        /// </summary>
        public TileMap()
        {
            DrawBackgroundLayer = true;
            DrawBaseLayer = true;
            DrawObjectLayer = true;
            BackgroundLayer = new List<Texture2D>();
        }


        public TileMap(string tilesSet, int rows, int columns)
        {
            Dimensions = new Point(columns, rows);
            TileSheetAssestName = tilesSet;
            TileSetTexture = TextureManager.SetTexture(tilesSet);
            BaseLayer = new int[rows * columns];
            CollisionLayer = new int[rows * columns];
            //Reset the base layer.
            for (int i = 0; i < BaseLayer.Length; i++)
                BaseLayer[i] = -1;

            CollisionTileTexture = TextureManager.SetTexture("square");
            CollisionTileTextureName = "square";
            DrawBackgroundLayer = true;
            DrawBaseLayer = true;
            DrawObjectLayer = true;
        } 

        #endregion
       
        #region Draw
        /// <summary>
        /// Draw's the map and all it's layers: Base,Object,Background,Collision.
        /// </summary>
        /// <param name="sb"></param>
        public virtual void Draw(SpriteBatch sb)
        {
            if (!DrawBaseLayer && !DrawObjectLayer && !DrawBackgroundLayer && !DrawCollisionLayer)
                return;

          //  sb.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend,null,null,null,null,Camera.Transform);
            sb.Begin();
            //Draw the background layer.
            if (DrawBackgroundLayer)
                foreach (Texture2D tex in BackgroundLayer)
                    sb.Draw(tex, Vector2.Zero, Color.White);

            for (int y = 0; y < Dimensions.Y; y++)
                for (int x = 0; x < Dimensions.X; x++)
                {
                    //Draw the base layer.
                    if (DrawBaseLayer)
                    {
                        Rectangle sourceRectangle = GetBaseLayerSourceRectangle(x, y);
                        if (sourceRectangle != Rectangle.Empty)
                            sb.Draw(TileSetTexture, Tile.Size * new Vector2(x, y), sourceRectangle, Color.White);
                    }
                    //Draw the collision layer.
                    if (DrawCollisionLayer && GetTileCollision(x, y) != TileCollision.Passable)
                    {
                        Color color = GetTileCollision(x, y) == TileCollision.Impassable ? Color.Red : Color.Blue;
                        sb.Draw(CollisionTileTexture, Tile.Size * new Vector2(x, y), color * 0.4f);
                    }
                   
                //    if (DrawGrid)//Draw the grid.
                //        sb.Draw(rect, new Rectangle((int)(Tile.Size * new Vector2(x, y)).X, (int)(Tile.Size * new Vector2(x, y)).Y, Tile.Width, Tile.Height), Color.White);
                }
            //Draw the object layer.
            if (DrawObjectLayer)
                foreach (GameObject o in ObjectLayer)
                    o.Draw(sb);
            sb.End();
           
        }//Draw
        #endregion

        #region Methods
        /// <summary>
        /// Gets the bounding rectangle of a tile in the map.
        /// </summary>
        /// <param name="x">The x coordinate of the tile.(in tiles)</param>
        /// <param name="y">The y coordinate of the tile.(in tiles)</param>
        public Rectangle GetTileBounds(int x, int y)
        {
            return new Rectangle(x * Tile.Width, y * Tile.Height, Tile.Width, Tile.Height);
        }

       
        /// <summary>
        /// Returns the id of a tile in a given map position.
        /// </summary>
        /// <param name="x">The x coordinate of the position.</param>
        /// <param name="y">The y coordinate of the position.</param>
        /// <param name="layer">The layer you want to get the value form.</param>
        /// <returns></returns>
        public int GetTileID(int x, int y, int[] layer)
        {
            if ((x >= Dimensions.X || x < 0) ||
                (y >= Dimensions.Y || y < 0))
            {
                throw new IndexOutOfRangeException("index was out of the map's bounds");
            }
            return layer[y * Dimensions.X + x];
        }

        /// <summary>
        /// Sets a tile in a given layer to a new tile value.
        /// </summary>
        /// <param name="x">The x coordinate of the position.</param>
        /// <param name="y">The y coordinate of the position.</param>
        /// <param name="tileValue">The new tile value</param>
        /// <param name="layer">The layer in which the tile will be set.</param>
        public void SetTile(int x, int y,int tileValue, int[] layer)
        {
            if (x < 0 || y < 0) return;
            if (x > Dimensions.X || y > Dimensions.Y) return;
            layer[y * Dimensions.X + x] = tileValue;
        }
       

        /// <summary>
        /// Gets the source rectangle of a given tile from the base layer.
        /// </summary>
        /// <param name="x">The x coordinate of the tile.(in tiles)</param>
        /// <param name="y">The y coordinate of the tile.(in tiles)</param>
        public Rectangle GetBaseLayerSourceRectangle(int x, int y)
        {
            if ((x >= Dimensions.X || x < 0) ||
               (y >= Dimensions.Y || y < 0))
            {
                return Rectangle.Empty;//If out of bounds, return empty.
            }

            int tileValue = GetTileID(x, y, BaseLayer);//get the tile value
            int tilesPerRow = TileSetTexture.Width / Tile.Width;//how much tiles in a row.

            //if it's not a tile that we should draw,return empty.
            if (tileValue < 0) return Rectangle.Empty;
            
            int y2 = tileValue / tilesPerRow;//get the y coordinate.
            int x2 = tileValue % tilesPerRow;//get the x coordinate.
            return new Rectangle(x2 * Tile.Width, y2 * Tile.Height, Tile.Width, Tile.Height);
        }

        /// <summary>
        /// Checks if the given tile is blocked.
        /// </summary>
        /// <param name="x">The x coordinate of the tile.(in tiles)</param>
        /// <param name="y">The y coordinate of the tile.(in tiles)</param>
        public bool isBlocked(int x, int y)
        {
            if (x >= Dimensions.X || x < 0) return true;
            if (y >= Dimensions.Y || y < 0) return true;
            //if the collision tile is 0, no collision.
            return GetTileID(x,y,CollisionLayer) == 1;
        }

        /// <summary>
        /// Gets the TileCollision type of a tile.
        /// </summary>
        /// <param name="x">The x coordinate of the tile.(in tiles)</param>
        /// <param name="y">The y coordinate of the tile.(in tiles)</param>
        public TileCollision GetTileCollision(int x, int y)
        {
            if (x >= Dimensions.X || x < 0) return TileCollision.Impassable;
            if (y >= Dimensions.Y || y < 0) return TileCollision.Impassable;
            return (TileCollision)GetTileID(x, y, CollisionLayer);
        }

        /// <summary>
        /// Serialize the map to an XML file called "(MapName).xml"
        /// </summary>
        public void Serialize()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            using (XmlWriter writer = XmlWriter.Create(Name + ".xml", settings))
            {
                IntermediateSerializer.Serialize(writer, this, null);    
            }
        }

        public static TileMap Deserialize(string fileName)
        {
            TileMap res = new TileMap();
          
            using (XmlReader r = XmlReader.Create(fileName))
            {
                res = IntermediateSerializer.Deserialize<TileMap>(r, null);
            }
            res.BackgroundLayer = new List<Texture2D>();
            foreach (string name in res.BackgroundLayerTextures)
                res.BackgroundLayer.Add(TextureManager.SetTexture(name));
            foreach (GameObject o in res.ObjectLayer)
                o.Texture = TextureManager.SetTexture(o.TextureName);
            res.TileSetTexture = TextureManager.SetTexture(res.TileSheetAssestName);
            res.CollisionTileTexture = TextureManager.SetTexture(res.CollisionTileTextureName);
            
            return res;


        }

        #endregion




        #region Content Reader
        public class MapReader : ContentTypeReader<TileMap>
        {
            protected override TileMap Read(ContentReader input, TileMap existingInstance)
            {
                TileMap result = existingInstance;
                if (result == null) result = new TileMap();

                result.Name = input.ReadString();
                result.Dimensions = input.ReadObject<Point>();
                result.TileSize = input.ReadObject<Point>();
                result.TileSheetAssestName = input.ReadString();
                result.CollisionTileTextureName = input.ReadString();
                result.BackgroundLayerTextures = input.ReadObject<List<string>>();
                result.BaseLayer = input.ReadObject<int[]>();
                result.ObjectLayer = input.ReadObject<List<GameObject>>();
                result.CollisionLayer = input.ReadObject<int[]>();
                result.SpawnPoint = input.ReadVector2();

                foreach (string textureName in result.BackgroundLayerTextures)
                    result.BackgroundLayer.Add(input.ContentManager.Load<Texture2D>(textureName));
                result.TileSetTexture = input.ContentManager.Load<Texture2D>(result.TileSheetAssestName);
                result.CollisionTileTexture = input.ContentManager.Load<Texture2D>(result.CollisionTileTextureName);

                return result;
            }
        }
        #endregion
    }

    #region Content Writer
    [ContentTypeWriter]
    public class MapWriter : ContentTypeWriter<TileMap>
    {
        protected override void Write(ContentWriter output, TileMap value)
        {
            output.Write(value.Name);
            output.WriteObject<Point>(value.Dimensions);
            output.WriteObject<Point>(value.TileSize);
            output.Write(value.TileSheetAssestName);
            output.Write(value.CollisionTileTextureName);
            output.WriteObject<List<string>>(value.BackgroundLayerTextures);
            output.WriteObject<int[]>(value.BaseLayer);
            output.WriteObject<List<GameObject>>(value.ObjectLayer);
            output.WriteObject<int[]>(value.CollisionLayer);
            output.Write(value.SpawnPoint);
        }

        public override string GetRuntimeReader(Microsoft.Xna.Framework.Content.Pipeline.TargetPlatform targetPlatform)
        {
            return typeof(TileMap.MapReader).AssemblyQualifiedName;
        }
    }
    #endregion
}
