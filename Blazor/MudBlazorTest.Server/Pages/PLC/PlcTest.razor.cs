// using MudBlazor;
// using System.ComponentModel.DataAnnotations;
// using libplctag;

// namespace MudBlazorTest.Server.Pages.PLC;

// public partial class PlcTest
// {
//     public Color SelectedColor { get; set; } = Color.Info;
//     public static int plcTimeout = 5000;

//     private string? value1;
//     private string? value2;
//     private string? value3;

//     private string[] _tags =
//     {
//         "O0:0/1", "I1:0/1", "B3:0/1",

//     };
//     public int IntValue { get; set; }
//     public double DoubleValue { get; set; }
//     public decimal DecimalValue { get; set; }

//     readonly string _ipAddress = "192.168.0.23";
//     readonly string _path = "1,0";

//     public class TagModel
//     {
//         //Auto increment the Tag Id
//         [Key, Required]
//         public int ID { get; set; }

//         [Required]
//         public string? Name { get; set; }

//         public DebugLevel DebugLevel { get; set; } = DebugLevel.None;
//         public TimeSpan Timeout { get; set; } = TimeSpan.FromMilliseconds(plcTimeout);
//         public TimeSpan AutoUpdateTimer { get; set; }

//         public short Value { get; set; }

//     }

//     private async Task<IEnumerable<string>> Search1(string value)
//     {
//         // In real life use an asynchronous function for fetching data from an api.
//         await Task.Delay(500);

//         // if text is null or empty, show complete list
//         if (string.IsNullOrEmpty(value))
//             return _tags;
//         return _tags.Where(x => x.Contains(value, StringComparison.InvariantCultureIgnoreCase));
//     }

// }

// /* ---- form validation example 
// public partial class PlcTest
// {
//     [Inject] ISnackbar? Snackbar { get; set; }

//     MudForm? form;

//     OrderModelFluentValidator orderValidator = new OrderModelFluentValidator();

//     OrderDetailsModelFluentValidator orderDetailsValidator = new OrderDetailsModelFluentValidator();

//     OrderModel model = new OrderModel();

//     public class OrderModel
//     {
//         public string? Name { get; set; }
//         public string? Email { get; set; }
//         public string CCNumber { get; set; } = "4012 8888 8888 1881";
//         public AddressModel Address { get; set; } = new AddressModel();
//         public List<OrderDetailsModel> OrderDetails = new List<OrderDetailsModel>()
//         {
//             new OrderDetailsModel()
//                 {
//                     Description = "Perform Work order 1",
//                     Offer = 100
//                 },
//             new OrderDetailsModel()
//         };
//     }

//     public class AddressModel
//     {
//         public string? Address { get; set; }
//         public string? City { get; set; }
//         public string? Country { get; set; }
//     }

//     public class OrderDetailsModel
//     {
//         public string? Description { get; set; }
//         public decimal Offer { get; set; }
//     }

//     private async Task Submit()
//     {
//         await form!.Validate();

//         if (form.IsValid)
//         {
//             Snackbar!.Add("Submited!");
//         }
//     }

//     /// <summary>
//     /// A standard AbstractValidator which contains multiple rules and can be shared with the back end API
//     /// </summary>
//     /// <typeparam name="OrderModel"></typeparam>
//     public class OrderModelFluentValidator : AbstractValidator<OrderModel>
//     {
//         public OrderModelFluentValidator()
//         {
//             RuleFor(x => x.Name)
//                 .NotEmpty()
//                 .Length(1, 100);

//             RuleFor(x => x.Email)
//                 .Cascade(CascadeMode.Stop)
//                 .NotEmpty()
//                 .EmailAddress()
//                 .MustAsync(async (value, cancellationToken) => await IsUniqueAsync(value));

//             RuleFor(x => x.CCNumber)
//                 .NotEmpty()
//                 .Length(1, 100)
//                 .CreditCard();

//             RuleFor(x => x.Address.Address)
//                 .NotEmpty()
//                 .Length(1, 100);

//             RuleFor(x => x.Address.City)
//                 .NotEmpty()
//                 .Length(1, 100);

//             RuleFor(x => x.Address.Country)
//                 .NotEmpty()
//                 .Length(1, 100);

//             RuleForEach(x => x.OrderDetails)
//                 .SetValidator(new OrderDetailsModelFluentValidator());
//         }

//         private async Task<bool> IsUniqueAsync(string email)
//         {
//             // Simulates a long running http call
//             await Task.Delay(2000);
//             return email.ToLower() != "test@test.com";
//         }

//         public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
//         {
//             var result = await ValidateAsync(ValidationContext<OrderModel>.CreateWithOptions((OrderModel)model, x => x.IncludeProperties(propertyName)));
//             if (result.IsValid)
//                 return Array.Empty<string>();
//             return result.Errors.Select(e => e.ErrorMessage);
//         };
//     }

//     /// <summary>
//     /// A standard AbstractValidator for the Collection Object
//     /// </summary>
//     /// <typeparam name="OrderDetailsModel"></typeparam>
//     public class OrderDetailsModelFluentValidator : AbstractValidator<OrderDetailsModel>
//     {
//         public OrderDetailsModelFluentValidator()
//         {
//             RuleFor(x => x.Description)
//                 .NotEmpty()
//                 .Length(1, 100);

//             RuleFor(x => x.Offer)
//                 .GreaterThan(0)
//                 .LessThan(999);
//         }

//         public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
//         {
//             var result = await ValidateAsync(ValidationContext<OrderDetailsModel>.CreateWithOptions((OrderDetailsModel)model, x => x.IncludeProperties(propertyName)));
//             if (result.IsValid)
//                 return Array.Empty<string>();
//             return result.Errors.Select(e => e.ErrorMessage);
//         };
//     }
// } */
