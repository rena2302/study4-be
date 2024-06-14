using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using study4_be.Models;
using study4_be.Models.ViewModel;
using study4_be.Repositories;

namespace study4_be.Controllers.Admin
{
    public class VideoController : Controller
    {
        private readonly ILogger<VideoController> _logger;
        public VideoController(ILogger<VideoController> logger)
        {
            _logger = logger;
        }
        private readonly VideoRepository _videosRepository = new VideoRepository();
        public STUDY4Context _context = new STUDY4Context();
        public async Task<IActionResult> Video_List()
        {
            try
            {
                var videos = await _context.Videos
                    .Include(v => v.Lesson)
                    .ThenInclude(l => l.Container)
                        .ThenInclude(c => c.Unit)
                            .ThenInclude(u => u.Course)
                    .ToListAsync();

                var videoViewModels = videos
                    .Select(video => new VideoListViewModel
                    {
                        video = video,
                        courseName = video.Lesson?.Container?.Unit?.Course?.CourseName ?? "N/A",
                        unitTittle = video.Lesson?.Container?.Unit?.UnitTittle ?? "N/A",
                        containerTittle = video.Lesson?.Container?.ContainerTitle ?? "N/A",
                        lessonTittle = video.Lesson?.LessonTitle ?? "N/A",
                    }).ToList();

                return View(videoViewModels);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error occurred while fetching vocabulary list.");

                // Handle the exception gracefully
                ModelState.AddModelError("", "An error occurred while processing your request. Please try again later.");
                return View(new List<VocabListViewModel>());
            }
        }
        public async Task<IActionResult> Video_Create()
        {
            var lessons = await _context.Lessons
                .Include(l => l.Container)
                .ThenInclude(c => c.Unit)
                    .ThenInclude(u => u.Course)
            .ToListAsync();

            var model = new VideoCreateViewModel
            {
                videos = new Video(),
                Lessons = lessons.Select(c => new SelectListItem
                {
                    Value = c.LessonId.ToString(),
                    Text = $"{c.LessonTitle} - Container: {(c.Container?.ContainerTitle ?? "N/A")} - Unit: {(c.Container?.Unit?.UnitTittle ?? "N/A")} - Course: {(c.Container?.Unit?.Course?.CourseName ?? "N/A")} - TAG: {(c.TagId ?? "N/A")}"
                }).ToList()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Video_Create(VideoCreateViewModel videoViewMode)
        {

            try
            {
                var video = new Video
                {
                    VideoUrl = videoViewMode.videos.VideoUrl,
                    LessonId = videoViewMode.videos.LessonId,
                    VideoId = videoViewMode.videos.VideoId,
                    //dont have video ID, need video_APIController to done
                };

                await _context.AddAsync(video);
                await _context.SaveChangesAsync();

                return RedirectToAction("Video_List", "Video");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating new unit.");
                ModelState.AddModelError("", "An error occurred while processing your request. Please try again later.");

                videoViewMode.Lessons = _context.Lessons.Select(c => new SelectListItem
                {
                    Value = c.LessonId.ToString(),
                    Text = c.LessonTitle.ToString()
                }).ToList();

                return View(videoViewMode);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVideoById(int id)
        {
            var video = await _context.Videos.FindAsync(id);
            if (video == null)
            {
                return NotFound();
            }

            return Ok(video);
        }

        public IActionResult Video_Edit()
        {
            return View();
        }

        public IActionResult Video_Delete()
        {
            return View();
        }

        public IActionResult Video_Details()
        {
            return View();
        }
        public IActionResult BuyCourse()
        {
            return View("test");
        }
    }
}
