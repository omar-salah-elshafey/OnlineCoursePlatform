namespace OnlineCoursePlatform.Application.Features.EnrollmentFeature.Dtos
{
    public class UpdateEnrollmentDto
    {

        public int Progress { get; set; } // Allow updating progress percentage
        public bool IsCompleted { get; set; } // Allow marking as completed or not
        public DateTime? CompletionDate { get; set; } // Allow setting/changing the completion date
    }
}
