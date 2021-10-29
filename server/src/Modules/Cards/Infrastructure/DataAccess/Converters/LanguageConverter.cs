using System;
using System.Linq.Expressions;
using Cards.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Cards.Infrastructure
{
    internal class LanguageConverter : ValueConverter<Language, int>
    {
        private static Expression<Func<Language, int>> _toDbValue => language => (int)language.Type;
        private static Expression<Func<int, Language>> _fromDbValue => dbValue => Language.Create((LanguageType)dbValue);
        public LanguageConverter() : base(_toDbValue, _fromDbValue, null) { }
    }
}