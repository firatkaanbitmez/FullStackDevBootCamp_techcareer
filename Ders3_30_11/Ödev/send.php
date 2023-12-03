<?php
if ($_SERVER["REQUEST_METHOD"] == "POST") {
    $name = $_POST['ad'];
    $email = $_POST['email'];
    $subject = $_POST['konu'];
    $message = $_POST['mesaj'];

    $to = "me@firatbitmez.com"; 
    $headers = "From: $email";

    mail($to, $subject, $message, $headers);
}
?>
