<?php

$servername = "remotemysql.com";
$username = "50L1stRsGD";
$password = "3KTyCInq3R";
$dbname = "50L1stRsGD";

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);

if ($_SERVER["REQUEST_METHOD"] == "GET") {
    if (isset($_GET["m_userId"])) {
        $stmt = $conn->prepare("SELECT * FROM Scoreboard WHERE m_userId=" . $_GET["m_userId"]);
        $stmt->execute();
        $result = $stmt->get_result();
        $outp = $result->fetch_all(MYSQLI_ASSOC);
        echo json_encode($outp);
    } else if (isset($_GET["m_username"])) {
        $stmt = $conn->prepare("SELECT * FROM Scoreboard WHERE m_username=" . $_GET["m_username"]);
        $stmt->execute();
        $result = $stmt->get_result();
        $outp = $result->fetch_all(MYSQLI_ASSOC);
        echo json_encode($outp);
    } else if (isset($_GET["all"])) {
        $stmt = $conn->prepare("SELECT * FROM Scoreboard LIMIT " . $_GET["all"]);
        $stmt->execute();
        $result = $stmt->get_result();
        $outp = $result->fetch_all(MYSQLI_ASSOC);
        echo json_encode($outp);
    }
} else if ($_SERVER["REQUEST_METHOD"] == "POST") {
    
    if (empty($_POST['m_userId']) || 
        empty($_POST['m_username']) || 
        empty($_POST['m_gamemode']) || 
        empty($_POST['m_score']) || 
        empty($_POST['m_hatId']) || 
        empty($_POST['m_faceId']) || 
        empty($_POST['m_colourId'])) {
        $returnObj = new \stdClass();
        $returnObj->result = false;
        $returnObj->reason = "Missing parameters";

        echo json_encode($returnObj);
    } else {

        $stmt = $conn->prepare("INSERT INTO Scoreboard VALUES (?, ?, ?, ?, ?, ?, ?)");
        $stmt->bind_param("issiiii", $_POST['m_userId'], $_POST['m_username'], $_POST['m_gamemode'], $_POST['m_score'], $_POST['m_hatId'], $_POST['m_faceId'], $_POST['m_colourId']);
        $stmt->execute();
        
        $returnObj = new \stdClass();
        $returnObj->result = true;
        $returnObj->reason = $stmt->affected_rows . " row(s) affected";

        echo json_encode($returnObj);
    }
}

$conn->close();
?>
