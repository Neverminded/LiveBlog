using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBlog.SearchEngine
{
    public abstract class SearchItem
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public abstract int Id { get; }

        /// <summary>
        /// Название CSS стиля для отображения
        /// </summary>
        public abstract string CssClass { get; }

        /// <summary>
        /// Наименование типа,
        /// используется для перенаправления
        /// пользователя
        /// </summary>

        public abstract string TypeName { get; }

        /// <summary>
        /// Заголовок, отображается в списке autocomplete
        /// </summary>
        public abstract string Title { get; }
    }
}