// <copyright file="ICsvParser.cs" company="HomeAppliancesStore">
// Copyright (c) HomeAppliancesStore. All rights reserved.
// </copyright>

namespace HomeAppliancesStore.Shared
{
    public interface ICsvParser<T>
    {
        T Parse(string line);

        string ToCsv(T entity);
    }
}
