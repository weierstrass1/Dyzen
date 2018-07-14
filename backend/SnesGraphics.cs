﻿using System.IO;
using System.Drawing;

namespace backend
{
    public class SnesGraphics
    {
        /// <summary>
        /// Obtain a matrix that have the colors used in each pixel.
        /// Only works for 4bpp gfx.
        /// </summary>
        /// <param name="path">Path of the gfx that you want to load.</param>
        /// <returns></returns>
        public static byte[,] generateGFX(string path)
        {

            byte[] gfx = File.ReadAllBytes(path);
            return generateGFX(gfx);
        }

        /// <summary>
        /// Obtain a matrix that have the colors used in each pixel.
        /// Only works for 4bpp gfx.
        /// </summary>
        /// <param name="gfx">An array of bytes that was read from .bin file</param>
        /// <returns></returns>
        public static byte[,] generateGFX(byte[] gfx)
        {
            byte[,] bits = getBits(gfx);

            byte[] colors = new byte[gfx.Length * 2];

            for (int i = 0; i < gfx.Length; i += 32)
            {
                for (int w = 0; w < 16; w+=2)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        colors[i * 2 + w * 4 + (7 - j)] = (byte)((bits[j, i + w]) +
                            (bits[j, i + w + 1] * 2) +
                            (bits[j, i + w + 16] * 4) +
                            (bits[j, i + w + 17] * 8));
                    }
                }
            }
            return GenerateColorMatrixFromColorArray(colors);
        }

        /// <summary>
        /// Generates an image from the byte matrix generated by GenerateGFX
        /// </summary>
        /// <param name="colors">Byte matrix obtained from GeneratedGFX</param>
        /// <param name="zoom">increase the size of the image.</param>
        /// <param name="palette">A color palette used to assign the colors</param>
        /// <returns></returns>
        public static Bitmap GenerateBitmapFromColorMatrix(byte[,] colors,int zoom, ColorPalette palette)
        {
            Bitmap bp = new Bitmap(colors.GetLength(0) * zoom, colors.GetLength(1) * zoom);

            for(int i = 0;i< colors.GetLength(0);i++)
            {
                for (int j = 0; j < colors.GetLength(1); j++)
                {
                    using (Graphics g = Graphics.FromImage(bp))
                    {
                        g.FillRectangle(new SolidBrush(palette.GetColor(colors[i, j])),
                        new RectangleF(i * zoom, j * zoom, zoom, zoom));
                    }
                }
            }
            return bp;
        }

        private static byte[,] GenerateColorMatrixFromColorArray(byte[] colors)
        {
            int height = colors.Length / 128;
            if (colors.Length % 1024 != 0) height = ((colors.Length / 1024) + 1) * 1024;
            byte[,] cols = new byte[128, height];

            for (int i = 0, j = 0, k = 0; i < colors.Length; i++)
            {
                j = ((i % 8) + 8 * (i / 64)) % 128;
                k = ((i / 8) % 8) + 8 * (i / 1024);

                cols[j, k] = colors[i];
            }
            return cols;
        }

        /// <summary>
        /// Obtain a GFX from a color matrix.
        /// Only work for 4bpp gfxs.
        /// </summary>
        /// <param name="colors">The Color Matrix obtained from GenerateGFX</param>
        /// <returns></returns>
        public static byte[] GetGFXFromColorMatrix(byte[,] colors)
        {
            byte[] gfx = new byte[(colors.GetLength(0) * colors.GetLength(1)) / 2];
            int k;
            byte b0, b1, b2, b3;

            for (int i = 0, x = 0, y = 0; i < gfx.Length; i += 32, x = (x + 8) % 128)
            {
                y = i / 512;
                for (int j = 0; j < 16; j += 2, y++)
                {
                    k = i + j;
                    gfx[k] = 0;
                    gfx[k + 1] = 0;
                    gfx[k + 16] = 0;
                    gfx[k + 17] = 0;

                    for (int p = 0, m = 128; p < 8; p++, m /= 2)
                    {
                        b0 = (byte)(colors[x + p, y] & 1);
                        b1 = (byte)((colors[x + p, y] & 2) >> 1);
                        b2 = (byte)((colors[x + p, y] & 4) >> 2);
                        b3 = (byte)((colors[x + p, y] & 8) >> 3);

                        gfx[k] += (byte)(b0 * m);
                        gfx[k + 1] += (byte)(b1 * m);
                        gfx[k + 16] += (byte)(b2 * m);
                        gfx[k + 17] += (byte)(b3 * m);
                    }
                }
            }

            return gfx;
        }

        /// <summary>
        /// Separate each bit of each byte on a byte array
        /// </summary>
        /// <param name="bytes">Byte array that you want to separate each bit</param>
        /// <returns></returns>
        public static byte[,] getBits(byte[] bytes)
        {
            byte[,] bits = new byte[8, bytes.Length];
            bool[] comp = new bool[bytes.Length];

            for (int i = 0; i < bytes.Length; i++)
            {
                bits[0, i] = (byte)(bytes[i] & 0x00000001);
                bits[1, i] = (byte)((bytes[i] & 0x00000002) >> 1);
                bits[2, i] = (byte)((bytes[i] & 0x00000004) >> 2);
                bits[3, i] = (byte)((bytes[i] & 0x00000008) >> 3);
                bits[4, i] = (byte)((bytes[i] & 0x00000010) >> 4);
                bits[5, i] = (byte)((bytes[i] & 0x00000020) >> 5);
                bits[6, i] = (byte)((bytes[i] & 0x00000040) >> 6);
                bits[7, i] = (byte)((bytes[i] & 0x00000080) >> 7);
            }

            return bits;
        }
    }
}