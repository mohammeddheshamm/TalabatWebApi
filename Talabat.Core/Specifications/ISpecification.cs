using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    // We have done this condition as we need all the entities that have been made  Tables
    public interface ISpecification<T> where T : BaseEntity
    {
        // Delegate
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T , object>>> Includes { get; set; }
        public Expression<Func<T , object>> OrderBy { get; set; } //P => P.Name
        public Expression<Func<T, object>> OrderByDescending { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginationEnabled { get; set; }
    }
}
