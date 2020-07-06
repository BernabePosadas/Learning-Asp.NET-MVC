import React from 'react';
import ReactDOM from 'react-dom';

class CommentBox extends React.Component {
    
    constructor(props) {
        super(props);
        this.state = {
            data : []
        };
        this._isMounted = false;
    }
    componentDidMount() {
            this._isMounted = true;
            let apiUrl = window.location.protocol + "//" + window.location.host + "/TaskList/List";
            fetch(apiUrl, { method: 'post' })
                .then(res => res.json())
                .then((response) => {
                    let state = {
                        data: response
                    };
                    this.setState(state);
                });
    }
    componentWillUnmount() {
        this._isMounted = false;
    }
    UpdateTask(item) {
        if (item.IsDone === "yes") {
            item.IsDone = "no";
        }
        else {
            item.IsDone = "yes";
        }
        var myHeaders = new Headers();
        myHeaders.append("Content-Type", "application/json");
        var raw = JSON.stringify(item);
        var requestOptions = {
            method: 'POST',
            headers: myHeaders,
            body: raw,
            redirect: 'follow'
        };

        let apiUrl = window.location.protocol + "//" + window.location.host + "/TaskList/Update";
        fetch(apiUrl, requestOptions)
            .then(res => res.json())
            .then((response) => {
                if (response.Status !== "200") {
                    alert(response.Message);
                }
                else {
                    let state = {
                        data: response.Payload
                    };
                    this.setState(state);
                }
            });
    }
    DeleteTask(GUID) {
        let apiUrl = window.location.protocol + "//" + window.location.host + "/TaskList/Delete?GUID=" + GUID;
        fetch(apiUrl, {
            method: 'delete'
        })
            .then(res => res.json())
            .then((response) => {
                if (response.Status !== "200") {
                    alert(response.Message);
                } else {
                    let state = {
                        data: response.Payload
                    };
                    this.setState(state);
                }
            });
    }
    checkBox(item) {
        if (item.IsDone === "yes") {
            return (<td><input type="checkbox" checked={true} onChange={(e) => this.UpdateTask(item)} /></td> );
        }
        else {
            return (
                <td><input type="checkbox" checked={false} onChange={(e) => this.UpdateTask(item)} /></td>
            );
        }
    }
    taskName(item) {
         
        if (item.IsDone === "yes") {
            return (<td><strike>{item.TaskName}</strike></td>);
        }
        else {
            return (
                <td>{item.TaskName}</td>
            );
        }
    }
    taskDescription(item) {
        if (item.IsDone === "yes") {
            return (<td><strike>{item.TaskDescription}</strike></td>);
        }
        else {
            return (
                <td>{item.TaskDescription}</td>
            );
        }
    }
    createTaskForm() {
        return (<div class="create-task-form form-inline">
            Task Name:&nbsp;&nbsp;
            <input
                id="TaskName"
                type="text"
                placeholder="Task Name"
                style={{ width: '20%' }}
                class="form-control"
            />&nbsp;&nbsp;
            Task Description:&nbsp;&nbsp;
            <input
                id="TaskDescription"
                type="text"
                placeholder="Task Description"
                class="form-control"
                style={{ width: '40%' }}
            />&nbsp;&nbsp;
            <button class="btn btn-success" onClick={(e) => this.buttonClick(e)} style={{ width: '20%' }}><i class="fa fa-save"></i>Add New Task</button>
        </div>);
    }
    buttonClick() {
        var name = document.getElementById("TaskName").value;
        var description = document.getElementById("TaskDescription").value;
        var payload = {
            TaskName: name,
            TaskDescription: description,
            IsDone : "no"
        };
        var myHeaders = new Headers();
        myHeaders.append("Content-Type", "application/json");
        var raw = JSON.stringify(payload);
        var requestOptions = {
            method: 'POST',
            headers: myHeaders,
            body: raw,
            redirect: 'follow'
        };

        let apiUrl = window.location.protocol + "//" + window.location.host + "/TaskList/Create";
        fetch(apiUrl, requestOptions)
            .then(res => res.json())
            .then((response) => {
                if (response.Status !== "200") {
                    alert(response.Message);
                }
                else {
                    document.getElementById("TaskName").value = "";
                    description = document.getElementById("TaskDescription").value = "";
                    let state = {
                        data: response.Payload
                    };
                    this.setState(state);
                }
            });
    }
    render() {
        return (
            <div class="render-body">
                {this.createTaskForm()}
                <table>
                    <tr>
                        <th></th>
                        <th>Task Name</th>
                        <th>Task Description</th>
                        <th></th>
                    </tr>
                    {this.state.data.map(item => (
                        <tr>
                            {this.checkBox(item)}
                            {this.taskName(item)}
                            {this.taskDescription(item)}
                            <td><button class="btn btn-danger" onClick={(e) => this.DeleteTask(item.ItemGUID)}><i class="fa fa-trash"></i>Delete</button></td>
                        </tr>
                    ))}
                    </table>
                </div>
        );
    }
}

ReactDOM.render(<CommentBox />, document.getElementById('root'));
