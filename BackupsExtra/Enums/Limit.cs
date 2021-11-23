namespace BackupsExtra.Enums
{
    public enum Limit
    {
        /// <summary>
        /// По дате - ограничивает насколько старые точки будут хранится
        /// (очищаем все точки, которые были сделаны до указанной даты)
        /// </summary>
        DateLimit,

        /// <summary>
        /// По количеству рестор поинтов - ограничивает длину цепочки из рестор поинтов
        /// (храним последние N рестор поинтов и очищаем остальные)
        /// </summary>
        RestorePoints,

        /// <summary>
        /// Гибрид - возможность комбинировать лимиты
        /// </summary>
        Hybrid,

        /// <summary>
        /// common merge
        /// </summary>
        Merge,
    }
}