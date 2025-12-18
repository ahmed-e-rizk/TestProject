using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.DTO.Response;

namespace Web.BLL.Response
{
    public interface IResponse<T>
    {
        public bool IsSuccess { get; set; }
        public List<TErrorField> Errors { get; set; }
        public T Data { get; set; }
    }
}
