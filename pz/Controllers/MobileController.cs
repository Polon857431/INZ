using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using pz.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace pz.Controllers
{
    public class MobileController : Controller
    {



        private PzContext db = new PzContext();
        // private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        // GET: Moblie


        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpPost]
        public ActionResult Authenticate(string login, string password)
        {
            var user = UserManager.FindByEmail(login);
            var profile = db.Profiles.Single(p => p.Username == user.UserName);
            var token = Guid.NewGuid();
            profile.Token = token;
           // db.Entry(profile).State = EntityState.Modified;
            db.SaveChanges();

            var result = UserManager.PasswordHasher.VerifyHashedPassword(user.PasswordHash, password);
            // _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password)
            if (result == PasswordVerificationResult.Success)
            {
                return Json(new { Response = "success", token = token.ToString() });

            }

            return Json(new { Response = "false" });
        }

        public ActionResult Check(string data, string token)
        {

            
            var profile = db.Profiles.Single(p => p.Token.ToString() == token);
            var point = db.Points.Single(p => p.QR_code == data);

            if (point != null)
            {
                var spots = db.Spots.Where(s => s.Event.Members.Any(mb => mb.ProfileId == profile.ID));
                foreach (var s in spots)
                {
                    db.MemberSpots.Add(new Models.MemberSpots { ProfileId = profile.ID, SpotId = s.ID });
                }
                db.SaveChanges();

                return Json(new { Response = "succes" });
            }

            return Json(new { Response = "false" });
        }

    }
}
