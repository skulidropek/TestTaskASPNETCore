using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestTaskASPNETCore.Models
{
    public class SchoolingActivityModel
    {
        [Key]
        [Required]
        public string Id { get; set; }

        public DateTime StartTime { get; set; }

        // Устанавливаем внешний ключ на UserInfo
        [ForeignKey("UserInfo")]
        [Required]
        public string UserInfoId { get; set; }

        // Ссылка на объект UserInfo
        public UserInfo UserInfo { get; set; }

        public string AchievedEtape { get; set; }

        public DateTime AchievedTime { get; set; }

        public SchoolingActivityModel()
        {
            Id = Guid.NewGuid().ToString();
        }

        public void Edit(SchoolingActivityModel updatedSchoolingActivity)
        {
            StartTime = updatedSchoolingActivity.StartTime;
            AchievedEtape = updatedSchoolingActivity.AchievedEtape;
            AchievedTime = updatedSchoolingActivity.AchievedTime;
            UserInfo.Edit(updatedSchoolingActivity.UserInfo);
        }

        public SchoolingActivityModel CopyTo()
        {
            var scholing = new SchoolingActivityModel()
            {
                StartTime = StartTime,
                UserInfo = UserInfo.CopyTo(),
                AchievedEtape = AchievedEtape,
                AchievedTime = AchievedTime,
            };

            scholing.UserInfoId = scholing.UserInfo.Id;
            scholing.UserInfo.SchoolingActivityModel = this;

            return scholing;
        }
    }
}
