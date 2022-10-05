using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using tec_site.Controllers;
using tec_site.Models;
using tec_site.EmailService;
using AutoMapper;
using System.Linq.Expressions;

namespace tec_site.Pages.Account
{

    public class RegisterModel : PageModel
    {
        private readonly IMapper _mapper;
        private readonly ILogger<RegisterModel> _logger;
        private readonly ApplicationUserManager _userManager;
        private readonly EmailSender _emailSender;
        private readonly Data.tec_siteContext _context;

        public RegisterModel(ILogger<RegisterModel> logger, Data.tec_siteContext context)
        {
            var mapperconfig = new MapperConfiguration(cfg =>
                    cfg.CreateMap<UserRegistrationModel, ApplicationUser>()
                );

            _mapper = new Mapper(mapperconfig);

            _logger = logger;

            _context = context;
        }

        public void OnGet()
        {
            Console.WriteLine("Register page acessed");
        }

        public async void ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return;

            var result = await _userManager.ConfirmEmailAsync(user, token);
            return;
        }

        [BindProperty]
        public UserRegistrationModel? UserRegistrationModel { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine(UserRegistrationModel.ToString());
            Console.WriteLine(ModelState.IsValid);
            
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ApplicationUser user = new()
            {
                UserName = UserRegistrationModel.UserName,
                FirstName = UserRegistrationModel.FirstName,
                LastName = UserRegistrationModel.LastName,
                Email = UserRegistrationModel.Email,
                EmailConfirmed = false,
                Password = UserRegistrationModel.Password
            };

            Console.WriteLine(user);

            if (user != null)
            {
                Console.WriteLine(_context);
                Console.WriteLine(_context.users);
                Console.WriteLine(_context.users.Add(user));
                
            }
            int task = await _context.SaveChangesAsync();
            Console.WriteLine(task);
            
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            Console.WriteLine(token);
            var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account", new { token, email = user.Email }, Request.Scheme);
            Console.WriteLine(confirmationLink);
            Dictionary<string, string> nameadressdict = new Dictionary<string, string>();
            nameadressdict.Add(user.UserName, user.Email);
            Console.WriteLine(nameadressdict);
            var message = new Message(nameadressdict, "Confirmation email link", confirmationLink, null);
            Console.WriteLine(message);
            await _emailSender.SendEmailAsync(message);
            Console.WriteLine(user.Email);
            /*if (user.Email == "awsomejojop@gmail.com" || user.Email == "theenergeticconvention@gmail.com")
            {
                await _userManager.AddToRoleAsync(user, "Admin");
            }
            else
            {
                await _userManager.AddToRoleAsync(user, "Visitor");
            }
            */

            return RedirectToPage("./Index");
        }
    }
}
