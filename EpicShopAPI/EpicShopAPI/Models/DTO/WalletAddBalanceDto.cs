using System.ComponentModel.DataAnnotations;

namespace EpicShopAPI.Models.DTO
{
    public class WalletAddBalanceDto
    {
        [Required]
        public int Amount { get; set; }
    }
}
