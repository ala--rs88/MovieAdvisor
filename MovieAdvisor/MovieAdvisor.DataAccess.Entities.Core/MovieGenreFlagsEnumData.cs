namespace MovieAdvisor.DataAccess.Entities.Core
{
    using System;

    [Flags]
    public enum MovieGenreFlagsEnumData
    {
        Unknown = 0,

        Action = 1,

        Adventure = 2,

        Animation = 4,

        Children = 8,

        Comedy = 16,

        Crime = 32,

        Documentary = 64,

        Drama = 128,

        Fantasy = 256,

        FilmNoir = 512,

        Horror = 1024,

        Musical = 2048,

        Mystery = 4096,

        Romance = 8192,

        SciFi = 16384,

        Thriller = 32768,

        War = 65536,

        Western = 131072,
        Fa
    }
}
