using Microsoft.EntityFrameworkCore;

namespace odec.Server.Model.User.Abst.Interfaces.Refactor
{
    /// <summary>
    /// Прокси объект для контекста партнера
    /// </summary>
    /// <typeparam name="TPartner">тип партнера</typeparam>
    /// <typeparam name="TPartnerProgram">тип партнерской программы</typeparam>
    public interface IPartnersContext<TPartner, TPartnerProgram>
        where TPartner : class 
        where TPartnerProgram : class
    {
        /// <summary>
        /// Таблица партнеров
        /// </summary>
        DbSet<TPartner> Partners { get; set; }

        /// <summary>
        /// Таблица партнерских программ
        /// </summary>
        DbSet<TPartnerProgram> PartnerPrograms { get; set; }
    }
}
