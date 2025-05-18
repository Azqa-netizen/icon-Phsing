using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmailSpawner : MonoBehaviour
{

    public GameObject emailIconPrefab;
    public Transform spawnParent;
    public EmailController emailUIController;
    private bool isWaiting = false;
    private int currentIndex = 0;

    public class EmailData
    {
        public string sender;
        public string subject;
        public string content;
        public bool isPhishing;
    }

    public List<EmailData> emailList = new List<EmailData>();

    void Start()
    {
        LoadEmailData();
        ShuffleEmailList(); 
        SpawnNextEmail();
    }

    void LoadEmailData()
    {
        emailList = new List<EmailData>()
    {
        // ---------- EMAIL AMAN (NON-PHISHING) ----------
        new EmailData {
            sender = "Google <security.google@gmail.com>",
            subject = "Login Baru dari Chrome di Jakarta",
            content = "Halo,\n\nKami mendeteksi adanya login baru ke akun Google Anda melalui browser Chrome pada perangkat Windows dengan IP 36.72.1.200 dari Jakarta, Indonesia. Jika ini adalah Anda, tidak ada tindakan lebih lanjut yang diperlukan. Namun jika Anda tidak mengenali aktivitas ini, kami sarankan Anda mengganti kata sandi Anda segera dan meninjau aktivitas akun Anda melalui halaman keamanan akun Google.\n\nTerima kasih telah menjaga keamanan akun Anda.",
            isPhishing = false
        },
        new EmailData {
            sender = "Tokopedia <noreply@tokopedia.com>",
            subject = "Pesanan Anda Sedang Diproses",
            content = "Terima kasih telah berbelanja di Tokopedia.\n\nPesanan Anda dengan nomor invoice #INV/20250515/001234 telah kami terima dan sedang diproses oleh penjual. Estimasi pengiriman adalah 1–2 hari kerja. Anda dapat memantau status pesanan melalui halaman 'Daftar Transaksi' di aplikasi atau website resmi kami.\n\nPastikan alamat pengiriman Anda sudah benar dan siap menerima barang.\n\nSalam hangat,\nTokopedia Customer Service.",
            isPhishing = false
        },
        new EmailData {
            sender = "Netflix <billing@netflix.com>",
            subject = "Pembayaran Bulanan Berhasil",
            content = "Hai!\n\nTerima kasih telah terus menggunakan layanan Netflix. Pembayaran Anda sebesar Rp 186.000 untuk paket 'Premium Ultra HD' telah berhasil diproses menggunakan kartu debit ****1234 pada tanggal 15 Mei 2025.\n\nTidak ada tindakan lanjutan yang perlu Anda lakukan. Anda dapat meninjau histori pembayaran dan detail langganan Anda melalui halaman akun di aplikasi Netflix atau di website resmi kami.\n\nSelamat menonton!",
            isPhishing = false
        },
        new EmailData {
            sender = "Universitas <akademik@univ.ac.id>",
            subject = "Jadwal Ujian Akhir Semester",
            content = "Yth. Mahasiswa,\n\nKami informasikan bahwa jadwal Ujian Akhir Semester (UAS) telah tersedia dan dapat diakses melalui portal mahasiswa di alamat https://portal.univ.ac.id. Silakan login menggunakan NIM dan password Anda, kemudian buka menu Akademik > Jadwal Ujian.\n\nMohon untuk mencetak kartu ujian sebelum tanggal 20 Mei 2025. Kehadiran pada semua sesi ujian bersifat wajib.\n\nTerima kasih.\nSalam,\nBagian Akademik Universitas",
            isPhishing = false
        },
        new EmailData {
            sender = "Dosen Pembimbing <budi.dosen@gmail.com>",
            subject = "Review Proposal Bab 2",
            content = "Halo mahasiswa,\n\nSaya telah membaca bab 2 dari proposal tugas akhir Anda. Secara umum sudah cukup baik, namun terdapat beberapa catatan penting:\n\n1. Uraian metodologi masih terlalu umum – tolong tambahkan detail langkah-langkah pelaksanaan.\n2. Referensi masih kurang dari 10 sumber. Tambahkan literatur relevan.\n\nMohon revisi segera dan kirimkan kembali dalam format PDF sebelum hari Jumat minggu ini. Jika butuh bimbingan tambahan, silakan ajukan jadwal pertemuan.\n\nSalam,\nBudi Santoso, M.Kom.",
            isPhishing = false
        },
        new EmailData {
            sender = "GitHub <noreply@github.com>",
            subject = "Anda Ditambahkan ke Repositori",
            content = "Hi there,\n\nAnda telah ditambahkan sebagai kolaborator pada repositori privat 'cybersecurity-game' oleh pengguna: devteam-id. Anda sekarang memiliki akses penuh untuk clone, push, dan membuat pull request.\n\nKlik tombol di bawah ini untuk mengakses repositori:\nhttps://github.com/devteam-id/cybersecurity-game\n\nJika Anda tidak mengenal proyek ini, harap abaikan email ini.\n\nHappy coding!",
            isPhishing = false
        },
        new EmailData {
            sender = "Steam <support@steampowered.com>",
            subject = "Terima Kasih atas Pembelian Anda",
            content = "Terima kasih telah melakukan pembelian melalui Steam.\n\nDetail Transaksi:\n- Game: Resident Evil Village\n- Harga: Rp 299.000\n- Metode Pembayaran: Dana\n- Tanggal: 14 Mei 2025\n\nGame tersebut kini telah ditambahkan ke pustaka Anda dan siap dimainkan.\n\nJika pembelian ini tidak Anda lakukan, silakan kunjungi halaman bantuan Steam untuk informasi lebih lanjut.",
            isPhishing = false
        },
        new EmailData {
            sender = "Niagahoster <billing@niagahoster.co.id>",
            subject = "Pembayaran Hosting Telah Diterima",
            content = "Halo,\n\nPembayaran Anda untuk layanan hosting dan domain dengan invoice #203912 telah berhasil kami terima. Layanan Anda akan aktif hingga 14 Mei 2026.\n\nTerima kasih telah menggunakan layanan Niagahoster. Anda dapat mengelola domain dan email melalui panel klien.\n\nUntuk pertanyaan atau kendala teknis, silakan hubungi support@niagahoster.co.id kapan saja.\n\nSalam hangat,\nTim Niagahoster",
            isPhishing = false
        },
        new EmailData {
            sender = "Team Dev <tim.dev@gmail.com>",
            subject = "Rapat Sprint Planning",
            content = "Hi Tim,\n\nJangan lupa kita ada rapat Sprint Planning besok, Jumat pukul 09.00 WIB. Agenda kita:\n- Evaluasi sprint minggu ini\n- Penetapan backlog baru\n- Diskusi tantangan teknis\n\nLink Google Meet:\nhttps://meet.google.com/sprint-dev-meeting\n\nHarap hadir tepat waktu ya.\n\nSalam,\nProduct Owner",
            isPhishing = false
        },
        new EmailData {
            sender = "HRD Kantor <hrd.perusahaan@gmail.com>",
            subject = "Form Absen Bulanan",
            content = "Kepada seluruh karyawan,\n\nMohon untuk segera mengisi form kehadiran bulanan periode Mei 2025. Form tersedia di Google Drive perusahaan dan hanya bisa diakses dengan akun email kantor.\n\nPastikan data diisi dengan benar, paling lambat tanggal 5 Mei 2025 pukul 17.00 WIB.\n\nJika ada kendala akses, silakan hubungi bagian HRD.\n\nTerima kasih atas kerjasamanya.\n\nHormat kami,\nTim HRD",
            isPhishing = false
        },

        // ---------- EMAIL PHISHING ----------
        new EmailData {
            sender = "Goggle Security <account@secure-goggle.info>",
            subject = "Urgent: Account Will Be Locked!",
            content = "Your Goggle account has been flagged for unusual activity. If you do not verify within 24 hours, your account will be locked permanently.\n\nClick the link below to secure your account:\nhttp://verify-goggle.com/login\n\nDo not ignore this message. Failure to act will result in data loss.",
            isPhishing = true
        },
        new EmailData {
            sender = "Bank BCA <notifikasi@bca-alerts.net>",
            subject = "⚠ Akun Anda Dikunci!",
            content = "Kami mendeteksi aktivitas mencurigakan pada akun Anda. Untuk alasan keamanan, akun Anda telah dikunci sementara.\n\nSilakan login di https://bca-login.secure.id untuk membuka kembali akun Anda.\n\nJika Anda tidak memverifikasi dalam 1x24 jam, akun akan ditutup permanen.",
            isPhishing = true
        },
        new EmailData {
            sender = "Undian Resmi <info@iphone14-gratis.win>",
            subject = "Anda Pemenang Hadiah iPhone!",
            content = "Selamat! Email Anda terpilih sebagai pemenang iPhone 14 Pro Max.\n\nUntuk mengklaim hadiah, segera kunjungi:\nhttp://giveaway-indo.vip/claim\n\nIsi data lengkap Anda dan lakukan verifikasi. Penyerahan hadiah hanya berlaku hari ini saja.",
            isPhishing = true
        },
        new EmailData {
            sender = "Tokopedia Support <tokopedia@support-center.info>",
            subject = "Pembayaran Anda Ditahan",
            content = "Transaksi Anda telah ditahan karena akun Anda perlu diverifikasi ulang.\n\nUnduh form berikut dan isi data pribadi Anda:\nLampiran: form_verifikasi_tokopedia.xls\n\nKirim kembali form ke email ini untuk memproses pengiriman.",
            isPhishing = true
        },
        new EmailData {
            sender = "Billing Department <billing@untrusted-mail.com>",
            subject = "❗ Invoice Belum Dibayar",
            content = "Invoice Anda telah jatuh tempo. Untuk menghindari penalti, segera lakukan pembayaran.\n\nDownload invoice di sini:\nAttachment: invoice_maret2025.exe\n\nPastikan untuk menyelesaikan pembayaran hari ini juga.",
            isPhishing = true
        },
        new EmailData {
            sender = "Facebook Security <noreply@facebook-notice.org>",
            subject = "Suspicious Login Attempt",
            content = "Kami menemukan login mencurigakan dari perangkat baru. Segera amankan akun Anda:\n\nhttp://fb-check.info/security\n\nAbaikan email ini jika Anda merasa aman.",
            isPhishing = true
        },
        new EmailData {
            sender = "HRD-File <internal@secure-share.download>",
            subject = "Rahasia Perusahaan",
            content = "File penting perusahaan tersedia untuk Anda.\n\nBuka file: salary_employees.rar\n\nPassword file: HR2025\n\nMohon jangan bagikan file ini ke siapa pun.",
            isPhishing = true
        },
        new EmailData {
            sender = "PeopleFinder <contact@peoplefind-you.org>",
            subject = "Seseorang Mencari Anda",
            content = "20+ orang mencari nama Anda minggu ini!\n\nKlik tautan di bawah ini untuk melihat siapa saja yang mencarinya:\nhttp://people-find.net\n\nLayanan gratis hanya hari ini!",
            isPhishing = true
        },
        new EmailData {
            sender = "Google Verify <verify@google-protect.info>",
            subject = "Login from New Device",
            content = "We detected a login attempt from an unrecognized device.\n\nIf this was not you, protect your account immediately:\nhttp://google-protect.info\n\nIgnoring this may lead to account suspension.",
            isPhishing = true
        },
        new EmailData {
            sender = "Dokumen-Tagihan <invoice@fast-downloads.pro>",
            subject = "Unduh Invoice Sekarang",
            content = "Invoice terbaru Anda tersedia untuk diunduh.\n\nKlik file berikut:\nAttachment: invoice_April2025.scr\n\nPastikan Anda mengaktifkan makro agar file dapat dibuka dengan benar.",
            isPhishing = true
        }
    };
    }




    void ShuffleEmailList()
    {
        for (int i = 0; i < emailList.Count; i++)
        {
            EmailData temp = emailList[i];
            int randomIndex = Random.Range(i, emailList.Count);
            emailList[i] = emailList[randomIndex];
            emailList[randomIndex] = temp;
        }
    }

    public void SpawnNextEmail()
    {
        if (currentIndex >= emailList.Count) return;

        EmailData data = emailList[currentIndex];
        GameObject emailGO = Instantiate(emailIconPrefab, spawnParent);

        ItemEmail item = emailGO.GetComponent<ItemEmail>();
        if (item != null)
        {
            item.Setup(
                $"{data.sender}: {data.subject}",
                $"{data.subject}\n\n{data.content}",
                data.isPhishing,
                emailUIController,
                emailGO
            );
        }

        currentIndex++;
    }

    public void OnEmailAnswered()
    {
        StartCoroutine(DelayNextSpawn());
    }

    private IEnumerator DelayNextSpawn()
    {
        isWaiting = true;
        yield return new WaitForSeconds(2f);
        isWaiting = false;
        SpawnNextEmail();
    }

}
