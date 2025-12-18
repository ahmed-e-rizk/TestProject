using AutoMapper;
using Infrastructure.Repository;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Web.BLL.Response;
using Web.BLL.Validation.Auth;
using Web.Core.Entites;
using Web.DTO.Auth;
using Web.DTO.Auth.Setting;

namespace Web.BLL.Auth
{
    public class AuthBLL : IAuthBLL
    {

        private readonly IRepository<User> _userRepository;
        private readonly IRepository<RefreshToken> _refreshTokenRepository;
        private readonly IMapper _mapper;

        private readonly AuthSetting _authSetting;

        public AuthBLL(IRepository<User> userRepository, IMapper mapper, IOptions<AuthSetting> authSetting, IRepository<RefreshToken> refreshTokenRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _authSetting = authSetting.Value;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public IResponse<TokenResultDto> Login(LoginDto loginDto)
        {

            var result = new Response<TokenResultDto>();
            /// vaildtion
            var vaild =new  LoginDtoVailidation().Validate(loginDto);

            if (!vaild.IsValid)
            {
                result.CreateResponse(vaild.Errors);
                return result;
            }

            var user = _userRepository.Get(e => e.Email == loginDto.UserName);
            if (user == null)
            {

                result.AppendError(new DTO.Response.TErrorField { Message="user not found "});
                return result;
            }


           
            if (user.Password != loginDto.Password)
            {

                var rrr =result.AppendError(new DTO.Response.TErrorField { Message = "invalid password " });
                return rrr;
            }


            var token = GenerateJwtTokenAsync(user);
            var generatedRefreshToken =  GenerateRefreshTokenAsync(token.Jti, user.Id);

            return result.CreateResponse(new TokenResultDto { Token=token.Token,RefreshToken= generatedRefreshToken });
        }

        public bool Register(RegisterDto registerDto)
        {

            try
            {

                var map = _mapper.Map<User>(registerDto);
             var user=   _userRepository.Add(map);


            }
            catch (Exception ex)
            {
                //add logs
                return false;
            }
            return true;
        }
        private  JwtTokenDto GenerateJwtTokenAsync(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _authSetting.Jwt.Issuer,
                Subject = new ClaimsIdentity(new List<Claim>
                    {
                        new Claim("Id", user.Id.ToString()),
                        new Claim("Email", user.Email),
                        new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    }),
                Expires = DateTime.UtcNow.Add(_authSetting.Jwt.TokenExpiryTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_authSetting.Jwt.Secret)), SecurityAlgorithms.HmacSha256Signature)
            };


            var token = jwtTokenHandler.CreateToken(tokenDescriptor);

            var jwtToken = jwtTokenHandler.WriteToken(token);

            return new JwtTokenDto
            {
                Jti = token.Id,
                Token = jwtToken
            };
       
        }
        private string GenerateRefreshTokenAsync(string jti, int Id)
        {
            var refreshToken = new RefreshTokenDto
            {
                Jti = jti,
                UserId = Id,
                ExpireDate = DateTime.UtcNow.AddMonths(_authSetting.Jwt.RefreshToken.RefreshTokenExpiryInMonths),
                Token = $"{GenerateRandom(_authSetting.Jwt.RefreshToken.TokenLength)}{Guid.NewGuid()}"
            };
            var map = _mapper.Map<RefreshToken>(refreshToken);

            _refreshTokenRepository.Add(map);

            return refreshToken.Token;
        }
        private string GenerateRandom(int length)
        {
            var random = new Random();

            return new string(Enumerable.Repeat("0123456789ABCDEFGHIJ0123456789KLMNOPQRST0123456789UVWXYZ012345", length)
                                        .Select(s => s[random.Next(s.Length)])
                                        .ToArray());
        }

    }
}
