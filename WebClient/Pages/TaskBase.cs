using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebClient.Abstractions;

namespace WebClient.Pages
{
    public class TaskBase: ComponentBase
    {       
        protected List<TaskModel> taskModels = new List<TaskModel>();
        protected List<MenuItem> leftMenuItem = new List<MenuItem>();

        protected bool showCreator;
        protected bool isLoaded;

        [Inject]
        public ITaskDataService TaskDataService { get; set; }
        public IMemberDataService MemberDataService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            var result = await TaskDataService.GetAllTasks();

            var resultMember = await MemberDataService.GetAllMembers();

            if (result != null && result.Payload != null && result.Payload.Any())
            {                
                foreach (var item in result.Payload)
                {
                    var member = resultMember.Payload.Where(x => x.Id == item.AssignedToId).FirstOrDefault();
                    taskModels.Add(new TaskModel()
                    {
                        id = item.Id,
                        text = item.Subject,
                        isDone = item.IsComplete,
                        member = member != null ? new FamilyMember
                        {
                            id = member.Id,
                            firstname = member.FirstName,
                            lastname = member.LastName,
                            email = member.Email,
                            avtar = member.Avatar,
                            role = member.Roles
                        }:null,
                    }); ;
                }
            }
         
            for (int i = 0; i < taskModels.Count; i++)
            {
                //leftMenuItem.Add(new MenuItem
                //{
                //    iconColor = taskModels[i].member,
                //    label = taskModels[i].firstname,
                //    referenceId = taskModels[i].id
                //});
            }
            showCreator = true;
            isLoaded = true;
        }          

    }
}
