using System;

namespace Lessons.Domain.Lesson
{
    public enum LessonTypeEnum
    {
        Undefined = 0,
        Fiszki,
        Typing
    }

    public readonly struct LessonType
    {
        public LessonTypeEnum Type { get; }
        private LessonType(LessonTypeEnum type)
        {
            Type = type;
        }

        public static LessonType Create(int type)
        {
            if (type <= 0) throw new Exception($"{nameof(Type)} must be defined");
            return new LessonType((LessonTypeEnum)type);
        }
    }
}