using System;
using System.Linq.Expressions;
using Cards.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Cards.Infrastructure
{
    internal class DrawerConverter : ValueConverter<Drawer, int>
    {
        private static Expression<Func<Drawer, int>> _toDbValue => drawer => drawer.RealValue;
        private static Expression<Func<int, Drawer>> _fromDbValue => dbValue => Drawer.Create(dbValue);
        public DrawerConverter() : base(_toDbValue, _fromDbValue, null) { }
    }
}