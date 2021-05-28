$(document).ready(function () {
    var myUrl = $("#myUrl").val();    
    $('#CourseID').change(function () {
        $('#UnitID').empty().append('<option value= "">-- Select Unit --</option>');
        $('#TopicID').empty().append('<option value= "">-- Select Topic --</option>');
        $('#SubTopicID').empty().append('<option value= "">-- Select SubTopic --</option>');
        $('#LearningObjectiveID').empty().append('<option value= "">-- Select LearningObjective --</option>');
        $('#LearningOutcomeID').empty().append('<option value= "">-- Select LearningOutcome --</option>');
        $.ajax({
            type: "GET",
            url: myUrl + "GetUnits",
            datatype: "Json",
            data: { courseId: $('#CourseID').val() },
            success: function (data) {
                $.each(data, function (index, value) {
                    $('#UnitID').append('<option value="' + value.UnitID + '">' + value.Description + '</option>');
                });
            }
        });
    });

    $('#UnitID').change(function () {
        $('#TopicID').empty().append('<option value= "">-- Select Topic --</option>');
        $('#SubTopicID').empty().append('<option value= "">-- Select SubTopic --</option>');
        $('#LearningObjectiveID').empty().append('<option value= "">-- Select LearningObjective --</option>');
        $('#LearningOutcomeID').empty().append('<option value= "">-- Select LearningOutcome --</option>');
        $.ajax({
            type: "GET",
            url: myUrl + "GetTopics",
            datatype: "Json",
            data: { unitId: $('#UnitID').val() },
            success: function (data) {
                $.each(data, function (index, value) {
                    $('#TopicID').append('<option value="' + value.TopicID + '">' + value.Description + '</option>');
                });
            }
        });
    });

    $('#TopicID').change(function () {
        $('#SubTopicID').empty().append('<option value= "">-- Select SubTopic --</option>');
        $('#LearningObjectiveID').empty().append('<option value= "">-- Select LearningObjective --</option>');
        $('#LearningOutcomeID').empty().append('<option value= "">-- Select LearningOutcome --</option>');
        $.ajax({
            type: "GET",
            url: myUrl + "GetSubTopics",
            datatype: "Json",
            data: { topicId: $('#TopicID').val() },
            success: function (data) {
                $.each(data, function (index, value) {
                    $('#SubTopicID').append('<option value="' + value.SubTopicID + '">' + value.Description + '</option>');
                });
            }
        });

        $.ajax({
            type: "GET",
            url: myUrl + "GetLearningObjectives",
            datatype: "Json",
            data: { topicId: $('#TopicID').val() },
            success: function (data) {
                $.each(data, function (index, value) {
                    $('#LearningObjectiveID').append('<option value="' + value.LearningObjectiveID + '">' + value.Description + '</option>');
                });
            }
        });

        $.ajax({
            type: "GET",
            url: myUrl + "GetLearningOutcomes",
            datatype: "Json",
            data: { topicId: $('#TopicID').val() },
            success: function (data) {
                $.each(data, function (index, value) {
                    $('#LearningOutcomeID').append('<option value="' + value.LearningOutcomeID + '">' + value.Description + '</option>');
                });
            }
        });
    });


    $('#CompetencyID').change(function () {
        $('#SkillID').empty().append('<option value= "">-- Select Skill --</option>');
        $('#SubskillID').empty().append('<option value= "">-- Select SubSkill --</option>');
        $.ajax({
            type: "GET",
            url: myUrl + "GetSkills",
            datatype: "Json",
            data: { competencyId: $('#CompetencyID').val() },
            success: function (data) {
                $.each(data, function (index, value) {
                    $('#SkillID').append('<option value="' + value.SkillID + '">' + value.Description + '</option>');
                });
            }
        });
    });

    $('#SkillID').change(function () {        
        $('#SubskillID').empty().append('<option value= "">-- Select SubSkill --</option>');
        $.ajax({
            type: "GET",
            url: myUrl + "GetSubSkills",
            datatype: "Json",
            data: { skillId: $('#SkillID').val() },
            success: function (data) {
                $.each(data, function (index, value) {
                    $('#SubskillID').append('<option value="' + value.SubSkillID + '">' + value.Description + '</option>');
                });
            }
        });
    });

});