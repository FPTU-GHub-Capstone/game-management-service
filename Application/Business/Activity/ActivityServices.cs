﻿using DomainLayer.Constants;
using DomainLayer.Entities;
using RepositoryLayer.Repositories;

namespace ServiceLayer.Business;

public class ActivityServices : IActivityServices
{
    public readonly IGenericRepository<ActivityEntity> _activityRepo;
    
    public ActivityServices(IGenericRepository<ActivityEntity> activityRepo)
    {
        _activityRepo = activityRepo;
    }
    public async Task<ICollection<ActivityEntity>> List() {
        return await _activityRepo.ListAsync();
    }
    public async Task<ActivityEntity> Search(Guid activityId)
    {
        return await _activityRepo.FoundOrThrowAsync(activityId, Constants.ENTITY.ACTIVITY + Constants.ERROR.NOT_EXIST_ERROR);
    }
    public async Task Create(ActivityEntity activity)
    {
        await _activityRepo.CreateAsync(activity);
    }
    public async Task Update(ActivityEntity activity)
    {
        await _activityRepo.UpdateAsync(activity);
    }
    public async Task Delete(Guid activityId)
    {
        await _activityRepo.DeleteSoftAsync(activityId);
    }
}
