﻿using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Interfaces.IRepositories
{
    public interface ICourseRepository
    {
        Task<Course?> GetCourseByIdAsync(int id);
        Task<List<Course>> GetAllCoursesAsync();
        Task AddCourseAsync(Course course);
        Task DeleteCourseAsync(Course course);
        Task SaveChangesAsync();
    }
}
