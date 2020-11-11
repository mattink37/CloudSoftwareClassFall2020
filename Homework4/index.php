<?php
    if( ( isset($_SERVER['PHP_AUTH_USER'] ) && ( $_SERVER['PHP_AUTH_USER'] == "admin" ) ) AND
      ( isset($_SERVER['PHP_AUTH_PW'] ) && ( $_SERVER['PHP_AUTH_PW'] == "password" )) )
    {
        $name = $_SERVER['PHP_AUTH_USER'];
        print("Hello, $name<br><br>\n");
    }
    else
    {
        //Send headers to cause a browser to request
        //username and password from user
        header("WWW-Authenticate: " .
            "Basic realm=\"Matt Inkeles' Homework 4\"");
        header("HTTP/1.0 401 Unauthorized");

        //Show failure text, which browsers usually
        //show only after several failed attempts
        print("This page is protected by HTTP " .
            "Authentication.<br>\nUse <b>admin</b> " .
            "for the username, and <b>password</b> " .
            "for the password.<br>\n");
    }
?>

<head>
  <link rel="shortcut icon" href="favicon.ico">
</head>

<button onclick="myFunction()">Toggle Mode</button>
<!--------------GET-------------->
<div id="GET">
<h1>Student Grade Retrieval</h1>
<?php
try {
    $conn = new PDO("sqlsrv:server = tcp:cloudcomputingclassdb.database.windows.net,1433; Database = CloudComputingClassDB", "mattink37", "a:U7wp_a");
    $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
}
catch (PDOException $e) {
    print("Error connecting to SQL Server.");
    die(print_r($e));
}

$connectionInfo = array("UID" => "mattink37", "pwd" => "a:U7wp_a", "Database" => "CloudComputingClassDB", "LoginTimeout" => 30, "Encrypt" => 1, "TrustServerCertificate" => 0);
$serverName = "tcp:cloudcomputingclassdb.database.windows.net,1433";
$conn = sqlsrv_connect($serverName, $connectionInfo);
$tsql= "SELECT DISTINCT id FROM Grades";
$getResults = sqlsrv_query($conn, $tsql);

$data = array();
while ($row = sqlsrv_fetch_array($getResults, SQLSRV_FETCH_ASSOC))
{
  $data[] = $row;
}

echo "<form action=\"\">
        <select id=\"studentCombo\" name=\"students\" onchange=\"showStudent(this.value)\">
          <option value=\"\">Select a student:</option>";
          for ($i = 0; $i < count($data); $i++)
          {
            $studentID = implode(" ", $data[$i]);
            echo "<option value=" . $studentID . ">student " . $studentID . "</option>";
          }
echo "</select></form>";
?><div id="txtHint">Student grades will be listed here</div></div>

<!--------------POST-------------->
<div id="POST" style="display:none">
<h1>Student Grade Entry</h1>
<form onsubmit="submitGrade(studentIDField.value, studentGradeField.value)">
  <label for="studentIDField">Student id</label>
  <input type="number" id="studentIDField"></input>
  <label for="studentGradeField">Student grade</label>
  <input type="text" id="studentGradeField"></input>
  <input type="submit" value="Submit">
</form></div>


<script>
function myFunction() {
  let get = document.getElementById("GET");
  let post = document.getElementById("POST");

  if (get.style.display === "none") {
    get.style.display = "block";
    post.style.display = "none";
  } else {
    post.style.display = "block";
    get.style.display = "none";
  }
}
</script>

<script>
  function showStudent(str) {
    var xhttpGet;
    var response;
    var deserializedResponse;
    var grades = 0;
    var grade;
    if (str == "") {
      document.getElementById("txtHint").innerHTML = "";
      return;
    }
    xhttpGet = new XMLHttpRequest();
    xhttpGet.onreadystatechange = function () {
      response = this.responseText;
      deserializedResponse = JSON.parse(response);
      if (this.readyState == 4 && this.status == 200) {
        document.getElementById("txtHint").innerHTML = "Grades:<br>"; 
        var i;
        for (i = 0; i < deserializedResponse.length; i++) {
          grade = deserializedResponse[i].grade;
          document.getElementById("txtHint").innerHTML += grade + "<br>";
          grades += grade;
        }
        document.getElementById("txtHint").innerHTML += "Average: " + grades / deserializedResponse.length;
      }
    };
    xhttpGet.open("GET", "getStudent.php?q=" + str, true);
    xhttpGet.send();
  }
</script>

<script>
  function submitGrade(str1, str2) {
    var xhttpPost = new XMLHttpRequest();
    
    xhttpPost.onreadystatechange = function () {
        console.log("State Change");
    };
    xhttpPost.open("GET", "submitGrade.php?id=" + str1 + "&grade=" + str2, true);
    //xhttpPost.setRequestHeader('Authorization','Basic ' + Base64StringOfUserColonPassword);
    xhttpPost.send();
    sleep(500); //wait for sql changes to update
  }
  function sleep(milliseconds) {
  var start = new Date().getTime();
  for (var i = 0; i < 1e7; i++) {
    if ((new Date().getTime() - start) > milliseconds){
      break;
    }
  }
  window.location.reload(true); 
}
</script>

<br><br><footer>Created by Matt Inkeles</footer>