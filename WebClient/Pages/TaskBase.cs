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
       
        //protected void onAddItem()
        //{
        //    showCreator = true;
        //    StateHasChanged();
        //}

        //protected async Task onMemberAdd(FamilyMember familyMember)
        //{
        //   var result = await  MemberDataService.Create(new Domain.Commands.CreateMemberCommand()
        //    {
        //        Avatar = familyMember.avtar,
        //        FirstName = familyMember.firstname,
        //        LastName = familyMember.lastname,
        //        Email = familyMember.email,
        //        Roles = familyMember.role
        //    });

        //    if (result != null && result.Payload != null && result.Payload.Id != Guid.Empty)
        //    {
        //        members.Add(new FamilyMember()
        //        {
        //            avtar = result.Payload.Avatar,
        //            email = result.Payload.Email,
        //            firstname = result.Payload.FirstName,
        //            lastname = result.Payload.LastName,
        //            role = result.Payload.Roles,
        //            id = result.Payload.Id
        //        });

        //        leftMenuItem.Add(new MenuItem
        //        {
        //            iconColor = result.Payload.Avatar,
        //            label = result.Payload.FirstName,
        //            referenceId = result.Payload.Id
        //        });


        //        showCreator = false;
        //        StateHasChanged();
        //    }
        //}

    }
}
