using System;
using FizzWare.NBuilder;

namespace Api.Tests.Utils
{

    public static class UserBuilder
    {
        public static Guid Id => Guid.Parse("028e3ef5-161c-4f1a-b2c1-0fbcdc232a85");
        public static string Name => nameof(Name);
        public static string Password => nameof(Password);
        public static string Email => nameof(Email);
        public static string FirstName => nameof(FirstName);
        public static string Surname => nameof(Surname);
        public static DateTime CreationDate => new DateTime(2022, 1, 1);
        public static int Status => 1;
        public static DateTime ConfirmationDate => new DateTime(2022, 1, 1);
        public static DateTime LoginDate => new DateTime(2022, 1, 1);

        public static ISingleObjectBuilder<UserTest> Default =>
            Builder<UserTest>
                .CreateNew()
                .With(x => x.Id = Id)
                .With(x => x.Name = Name)
                .With(x => x.Password = Password)
                .With(x => x.Email = Email)
                .With(x => x.FirstName = FirstName)
                .With(x => x.Surname = Surname)
                .With(x => x.CreationDate = CreationDate)
                .With(x => x.Status = Status)
                .With(x => x.ConfirmationDate = ConfirmationDate)
                .With(x => x.LoginDate = LoginDate);
    }
}