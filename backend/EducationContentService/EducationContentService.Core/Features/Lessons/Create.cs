using CSharpFunctionalExtensions;
using EducationContentService.Domain.Lessons;
using EducationContentService.Domain.Shared;
using EducationContentService.Domain.ValueObjects;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace EducationContentService.Core.Features.Lessons;

public record CreateLessonRequest(string Title, string Description);

public sealed class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/lessons", async (
            [FromBody] CreateLessonRequest request, 
            CreateHandler handler) =>
        {
            var result = await handler.Handle(request);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.Ok(result.Value);
        });
    }
}

public sealed class CreateHandler(
    ILogger<CreateHandler> logger,
    ILessonsRepository lessonsRepository)
{
    public async Task<Result<Guid, Error>> Handle(CreateLessonRequest request)
    {
        Result<Title, Error> titleResult = Title.Create(request.Title, nameof(Lesson));
        if (titleResult.IsFailure)
            return titleResult.Error;

        Result<Description, Error> descriptionResult = Description.Create(request.Description, nameof(Lesson));
        if (descriptionResult.IsFailure)
            return descriptionResult.Error;

        var lesson = new Lesson(Guid.NewGuid(), titleResult.Value, descriptionResult.Value);

        Result<Guid, Error> result = await lessonsRepository.AddAsync(lesson);
        if (result.IsFailure)
            return result.Error;

        logger.LogInformation("Created lesson {Id}", lesson.Id);

        return lesson.Id;
    }
}