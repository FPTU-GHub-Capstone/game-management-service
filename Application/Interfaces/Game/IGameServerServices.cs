﻿using Domain.Entities.Game;

namespace Application.Interfaces; 
public interface IGameServerServices {
    //Game Server
    Task <ICollection<GameServer>> List();
    Task <GameServer> GetById(Guid gameServerId);
    Task<ICollection<GameServer>> GetByGameId(Guid gameId);
    Task<int> Count();
    Task Create(GameServer gameServer);
    Task Update(Guid gameServerId, GameServer gameServer);
    Task Delete(Guid gameServerId);
}
