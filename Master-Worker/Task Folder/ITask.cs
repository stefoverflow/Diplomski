using System;

namespace Master_Worker.Task_Folder
{
    public interface ITask
    {
        bool Run(string current_task_path);
        Guid GetTaskId();
        Guid GetClientId();
        string GetTaskType();
        string GetData();
    }
}
