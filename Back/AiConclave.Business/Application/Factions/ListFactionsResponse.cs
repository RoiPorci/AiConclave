using System.Collections.Generic;
using AiConclave.Business.Application.Factions.DTOs;

namespace AiConclave.Business.Application.Factions;

public class ListFactionsResponse : BaseResponse
{
    public List<FactionDto> Factions { get; set; }
}