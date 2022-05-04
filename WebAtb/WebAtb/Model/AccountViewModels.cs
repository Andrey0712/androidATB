namespace WebAtb.Model
{
    public class RegisterViewModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Photo { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class LoginUserModel
    {
        
        /// <summary>
        /// Емейл користувача
        /// </summary>
        /// <example>gg@gg.gg</example>
        public string Email { get; set; }
        /// <summary>
        /// Пароль користувача
        /// </summary>
        /// <example>12345</example>
        public string Password { get; set; }
    }


    public class UserItemViewModel
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Photo { get; set; }
        public string Phone { get; set; }
    }
    //тип для токена в контролер логин Example Value Schema в документацию свагера
    
    public class TokenResponceViewModel
    {
        /// <summary>
        /// token
        /// </summary>
        /// <example>eyJpZCI6IjEzMzciLCJ1c2VybmFtZSI6ImJpem9uZSIsImlhdCI6MTU5NDIwOTYwMCwicm9sZSI6InVzZXIifQ</example>
        public string token { get; set; }
    }

    public class UserEditViewModel
    {
        //public long Id { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string SecondName { get; set; }
        public string Photo { get; set; }
        public string Phone { get; set; }
    }
     public class UserDelViewModel
    {
        public long Id { get; set; }
        
    }
}
