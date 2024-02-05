using System.Threading.Tasks;
using billservice.models;



namespace billservice.services
{
    /// <summary>
    /// 用户服务
    /// </summary>
    public class UserService : interfaces.IUser
    {
        readonly IRepository.IUser iuser;


        /// <summary>
        /// 注入
        /// </summary>
        /// <param name="iuser"></param>
        public UserService ( IRepository.IUser iuser )
        {
            this.iuser = iuser;
        }



        public users GetUser ( string mobile )
        {

            return this.iuser.GetUser( mobile );

        }



        public async Task<users> GetUserAsync ( string mobile )
        {

            return await this.iuser.GetUserAsync( mobile );
        }



        public bool IsExistEmail ( string email )
        {


            return this.iuser.IsExistEmail( email );
        }



        public bool IsExistUser ( string mobile )
        {
            return this.iuser.IsExistUser( mobile );
        }



        public bool Save ( users user )
        {


            return this.iuser.Save( user );
        }



        public async Task<bool> SaveAsync ( users user )
        {


            return await this.iuser.SaveAsync( user );
        }



        /// <summary>
        /// 修改头像
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="avatar"></param>
        /// <returns></returns>
        public bool UpdateAvatar ( string mobile , string avatar )
        {


            return this.iuser.UpdateAvatar( mobile , avatar );
        }



        public async Task<bool> UpdateAvatarAsync ( string mobile , string avatar )
        {


            return await this.iuser.UpdateAvatarAsync( mobile , avatar );
        }



        public bool UpdateLoginInfo ( string mobile , int logintimes )
        {


            return this.iuser.UpdateLoginInfo( mobile , logintimes );
        }



        public async Task<bool> UpdateLoginInfoAsync ( string mobile , int logintimes )
        {


            return await this.iuser.UpdateLoginInfoAsync( mobile , logintimes );
        }



        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public bool UpdatePassWord ( string mobile , string pwd )
        {


            return this.iuser.UpdatePassWord( mobile , pwd );
        }



        public async Task<bool> UpdatePassWordAsync ( string mobile , string pwd )
        {


            return await this.iuser.UpdatePassWordAsync( mobile , pwd );
        }


    }
}

