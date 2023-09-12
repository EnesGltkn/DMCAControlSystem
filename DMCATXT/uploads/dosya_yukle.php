<?php
if ($_SERVER['REQUEST_METHOD'] === 'POST') {
  $dosya = $_FILES['dosya'];
  $hedef_dizin = 'C:/Users/burak/OneDrive/Masaüstü/TXTUPLOADS/'; // Hedef dizin yolunu güncelleyin

  // Dosyayı hedef dizine kaydetme
  if (move_uploaded_file($dosya['tmp_name'], $hedef_dizin . $dosya['name'])) {
    // Dosya başarıyla kaydedildiğinde yapılacak işlemler
    $aciklama = $_POST['aciklama'];
    // Veritabanına kaydetme veya diğer işlemler burada gerçekleştirilebilir
    echo 'Dosya yükleme başarılı!';
  } else {
    echo 'Dosya yükleme hatası.';
  }
}
?>
